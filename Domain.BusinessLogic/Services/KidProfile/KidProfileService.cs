using AutoMapper;
using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System.Threading.Tasks;
using DataAccess.UserManagement;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Domain.DataAccess.Services;
using Domain.DataAccess.Entities.KidProfile;
using System.Collections.Generic;

namespace Domain.BusinessLogic.Services
{
    public class KidProfileService : BaseBusinessService, IKidProfileService
    {
        private readonly DataAccess.ServiceInterfaces.IUserRepository _userDomainService;
        private readonly IMediaApplicationService _mediaApplicationService;
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IKidProfileRepository _kidProfileRepository;
        private readonly IMapper _mapper;

        private readonly ImageSettings _imageSettings; // TODO: maybe move this to blob storage settings

        public KidProfileService(IHttpContextAccessor httpContextAccessor
            , IUserRepository userDomainService
            , IConfiguration configuration
            , IMediaApplicationService mediaApplicationService
            , ISocialLinkRepository socialLinkRepository
            , IMapper mapper
            , ICompanyRepository companyRepository
            , IKidProfileRepository kidProfileRepository
            ) : base(httpContextAccessor)
        {
            _userDomainService = userDomainService;
            _mediaApplicationService = mediaApplicationService;
            _socialLinkRepository = socialLinkRepository;
            _mapper = mapper;
            _kidProfileRepository = kidProfileRepository;
            _imageSettings = configuration.GetSection(nameof(ImageSettings)).Get<ImageSettings>();
        }


       
        public KidProfileDto FindById(int id)
        {
            var dbUser = _userDomainService.GetByIdAsync(id);
            return _mapper.Map<KidProfileDto>(dbUser);
        }


        public async Task<KidProfileDto> CreateAsync(KidProfileDto dto)
        {
            var profile = _mapper.Map<KidProfile>(dto);
            var result = await _kidProfileRepository.CreateAsync(profile, true);
            return _mapper.Map<KidProfileDto>(result);
        }

        public async Task<KidProfileDto> UpdateAsync(int id, KidProfileDto dto)
        {
            var entity = await _kidProfileRepository.GetByIdAsync(id);

            //if (dto.Image == null)
            //{ user.Image = dbUser.Image; }
            //else
            //{
            //    // Adding image of new user
            //    user.Image = await _mediaApplicationService.UploadMediaAsync(model.Image);
            //    //UpdateImage(dbUser.Id, imageName);
            //}

            entity.Bio = dto.Bio;
            entity.CityId = dto.CityId;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Impairment = dto.Impairment;
            //entity.Url 

            return _mapper.Map<KidProfileDto>(entity);
        }

        #region private methods

        private string GetUserImage(string image) {
            if (image == null) return String.Empty;

            var img = image.Split("/").Last();
            return $"{_imageSettings.Path}/{img}";
        }
        private async Task<UserDto> CreateAsync(User user, RegistrationModel regUser)
        {

            if (regUser.ImageFile != null)
            { 
                user.Image = await _mediaApplicationService.UploadMediaAsync(regUser.ImageFile); 
            }
            else
            { 
                user.Image = ""; 
            }

            return null;
        }

        private async Task CreateSocialLinksAsync(int userId, UpdateUserModel updateUser)
        {
            if (updateUser.SocialLinks != null)
            {
                var links = _socialLinkRepository.GetForUser(userId);
                await _socialLinkRepository.DeleteAsync(links.Select(sl => sl.Id).ToList());
                var list = updateUser.SocialLinks.Select((sl) => new ExternalLink()
                {
                    Name = sl.Name,
                    Url = sl.Link
                }).ToList();
                _socialLinkRepository.CreateAll(list);
            }
        }

        public async Task<IList<KidProfileDto>> FindByParentIdAsync(int parentId)
        {
            var list = await _kidProfileRepository.GetByParentId(parentId);
            return list.Select(t => _mapper.Map<KidProfileDto>(t)).ToList();
        }

        public async Task<KidProfileDto> FindByIdAsync(int id)
        {
            var item = await _kidProfileRepository.GetByIdAsync(id);
            var result = _mapper.Map<KidProfileDto>(item);
            return result;
        }

        #endregion
    }
}
