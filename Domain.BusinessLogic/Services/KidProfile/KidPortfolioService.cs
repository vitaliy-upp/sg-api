using AutoMapper;
using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Domain.DataAccess.Services;
using System.Collections.Generic;
using Domain.DataAccess.Entities.KidProfile;

namespace Domain.BusinessLogic.Services
{
    public class KidPortfolioService : BaseBusinessService,  IKidPortfolioService
    {
        private readonly IKidPortfolioRepository _repository;
        private readonly IKidProfileService _kidProfileService;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;


        public KidPortfolioService(IHttpContextAccessor httpContextAccessor
            , IMapper mapper
            , IKidPortfolioRepository repository
            , IKidProfileService kidProfileService
            , IAttachmentService attachmentService
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _kidProfileService = kidProfileService;
            _attachmentService = attachmentService;
        }

        public async Task CreateAsync(int kidId, int parentId, AttachmentDto dto)
        {
            var kid = await _kidProfileService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("Kid was not found");
            }

            var attachment = await _attachmentService.UploadAsync(dto);
            var item = new KidPortfolioItem() { 
                AttachmentId = attachment.Id,
                KidId = kidId
            };
            await _repository.CreateAsync(item);
        }

        public async Task DeleteAsync(int kidId, int parentId, int id)
        {
            var kid = await _kidProfileService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("Kid was not found");
            }

            await _repository.DeleteAsync(kidId, id, true);
        }

        public async Task<KidPortfolioDto> GetAsync(int kidId)
        {
            IList<KidPortfolioItem> list = await _repository.GetByKidId(kidId);
            var result = new KidPortfolioDto();
            var attachments = list.Select(t => _mapper.Map<AttachmentDto>(t.Attachment)).ToList();

            result.Audio = attachments
                .Where(a => a.Type == FileManagement.DataAccess.AttachmentTypeEnum.KidAudio)
                .ToList();

            result.Videos = attachments
                .Where(a => a.Type == FileManagement.DataAccess.AttachmentTypeEnum.KidVideo)
                .ToList();

            result.Photos = attachments
                .Where(a => a.Type == FileManagement.DataAccess.AttachmentTypeEnum.KidPhoto)
                .ToList();

            result.Links = attachments
                .Where(a => a.Type == FileManagement.DataAccess.AttachmentTypeEnum.KidLink)
                .ToList();

            result.Docs = attachments
                .Where(a => a.Type == FileManagement.DataAccess.AttachmentTypeEnum.KidDoc)
                .ToList();

            return result;
        }

        Task<IList<KidPortfolioDto>> IKidPortfolioService.GetAsync(int kidId)
        {
            throw new NotImplementedException();
        }
    }
}
