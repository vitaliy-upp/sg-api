using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.Enums;
using Domain.BusinessLogic.Extensions;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DataAccess.UserManagement;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Domain.DataAccess.Services;

namespace Domain.BusinessLogic.Services
{
    public class UserApplicationService : BaseApplicationService, IUserApplicationService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IMediaApplicationService _mediaApplicationService;
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly ICompanyRepository _сompanyRepository;
        private readonly IMapper _mapper;

        private readonly ImageSettings _imageSettings; // TODO: maybe move this to blob storage settings

        public UserApplicationService(IHttpContextAccessor httpContextAccessor
            , IUserDomainService userDomainService
            , IConfiguration configuration
            , IMediaApplicationService mediaApplicationService
            , ISocialLinkRepository socialLinkRepository
            , IMapper mapper
            , ICompanyRepository companyRepository
            ) : base(httpContextAccessor)
        {
            _userDomainService = userDomainService;
            _mediaApplicationService = mediaApplicationService;
            _socialLinkRepository = socialLinkRepository;
            _mapper = mapper;
            _сompanyRepository = companyRepository;
            _imageSettings = configuration.GetSection(nameof(ImageSettings)).Get<ImageSettings>();
        }


        public bool IfExists(string email)
        {
            return _userDomainService.GetByEmail(email) != null;
        }
        public async Task<UserModel> CreateAsync(RegistrationModel user)
        {
            var newuser = _mapper.Map<User>(user);
            return await CreateAsync(newuser, user);
        }
        public async Task<UserModel> CreateAsync(string email, URegistrationModel reguser)
        {
            var newuser = _mapper.Map<User>(reguser);
            newuser.Email = email;
            newuser.IsEmailVerified = true;
            var regModel = _mapper.Map<RegistrationModel>(reguser);
            return await CreateAsync(newuser, regModel);
        }
        public UserModel FindById(int id)
        {
            var dbUser = _userDomainService.GetById(id);
            return _mapper.Map<UserModel>(dbUser);
        }
        public void ConfirmEmail(int id)
        {
            var dbUser = _userDomainService.GetById(id);
            dbUser.IsEmailVerified = true;
            _userDomainService.Update(dbUser);
        }
        public UserModel FindByLoginPassword(string username, string password)
        {
            var dbUser = _userDomainService.GetByEmail(username);
            if (dbUser == null || !dbUser.IsEmailVerified)
            { return null; }

            if (VerifyPassword(dbUser, dbUser.Password, password))
            {
                var model = _mapper.Map<UserModel>(dbUser);
                int roleId = _userDomainService.GetUserRoleId(model.Id);
                model.Role = (UserRolesEnum)roleId;

                return model;
            }
            return null;
        }
        public UserModel FindByIdentity(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail)) return null;

            var dbUser = _userDomainService.GetByEmail(userEmail);
            dbUser.Image = GetUserImage(dbUser.Image);
            return _mapper.Map<UserModel>(dbUser);
        }
        public User FindDetailedByIdentity(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail)) return null;

            var user = _userDomainService.GetDetailedByEmail(userEmail);
            user.Image = GetUserImage(user.Image);
            return user;
        }

        public UserModel FindByEmail(string email)
        {
            var dbUser = _userDomainService.GetByEmail(email);
            return _mapper.Map<UserModel>(dbUser);
        }

        public UserModel GetUserProfileByIdentity(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail)) return null;
            var dbUser = _userDomainService.GetByEmail(userEmail);
            dbUser = _userDomainService.GetDetailedById(dbUser.Id);
            dbUser.Image = GetUserImage(dbUser.Image);
            return _mapper.Map<UserModel>(dbUser);
        }

        public async Task<UserModel> UpdateAsync(string userEmail, UpdateUserModel model)
        {
            var dbUser = _userDomainService.GetByEmail(userEmail);
            var user = _mapper.Map<User>(model);
            user.Id = dbUser.Id;
            user.IsEmailVerified = true;
            user.CompanyId = dbUser.CompanyId;
            if (model.Image == null)
            { user.Image = dbUser.Image; }
            else
            {
                // Adding image of new user
                user.Image = await _mediaApplicationService.UploadMediaAsync(model.Image);
                //UpdateImage(dbUser.Id, imageName);
            }

            UpdateCompany(user, model);

            _userDomainService.Update(user);

            CreateSocialLinks(user.Id, model);

            var updatedUser = _userDomainService.GetDetailedById(user.Id);
            var updatedUserModel = _mapper.Map<UserModel>(updatedUser);
            return updatedUserModel;
        }        

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var dbUser = _userDomainService.GetById(userId);
            if (VerifyPassword(dbUser, dbUser.Password, oldPassword))
            {
                dbUser.Password = HashPassword(dbUser, newPassword);
                _userDomainService.Update(dbUser);
            }
            else
            { throw new Exception("Password is not valid"); }
        }
        public void ChangePassword(int userId, string newPassword)
        {
            var dbUser = _userDomainService.GetById(userId);
            dbUser.Password = HashPassword(dbUser, newPassword);
            _userDomainService.Update(dbUser);
        }
        public void UpdateImage(int userId, string imageName)
        {
            var dbuser = _userDomainService.GetById(userId);
            if (dbuser == null) return;

            dbuser.Image = imageName;
            _userDomainService.Update(dbuser);
        }

        public bool IsRegistered()
        {
            return _userDomainService.IsRegistered(CurrentUserEmail);
        }


        #region private methods

        private string GetUserImage(string image) {
            if (image == null) return String.Empty;

            var img = image.Split("/").Last();
            return $"{_imageSettings.Path}/{img}";
        }
        private async Task<UserModel> CreateAsync(User user, RegistrationModel regUser)
        {
            // Getting a hash of password
            var passwdHash = HashPassword(user, regUser.Password);
            user.Password = passwdHash;

            if (regUser.ImageFile != null)
            { user.Image = await _mediaApplicationService.UploadMediaAsync(regUser.ImageFile); }
            else
            { user.Image = ""; }


            if (!string.IsNullOrEmpty(regUser.CompanyName))
            {
                var company = new Company()
                {
                    Name = regUser.CompanyName,
                    Website = regUser.CompanyUrl
                };
                company = _сompanyRepository.Create(company);
                user.CompanyId = company.Id;
            }

            _userDomainService.Create(user);

            _userDomainService.SetUserRole(user.Id, (int)UserRolesEnum.User);

            user = _userDomainService.GetByEmail(regUser.Email);

            return _mapper.Map<UserModel>(user);
        }

        private void CreateSocialLinks(int userId, UpdateUserModel updateUser)
        {
            if (updateUser.SocialLinks != null)
            {
                var links = _socialLinkRepository.GetForUser(userId);
                _socialLinkRepository.Delete(links.Select(sl => sl.Id).ToList());
                var list = updateUser.SocialLinks.Select((sl) => new ExternalLink()
                {
                    Name = sl.Name,
                    Url = sl.Link
                }).ToList();
                _socialLinkRepository.CreateAll(list);
            }
        }

        private void UpdateCompany(User user, UpdateUserModel model)
        {
            if (user.CompanyId == null)
            {
                if (String.IsNullOrEmpty(model.CompanyName) == false)
                {
                    var company = _сompanyRepository.Create(new Company()
                    {
                        Name = model.CompanyName,
                        Website = model.CompanyUrl
                    });
                    _сompanyRepository.SaveChanges();
                    user.CompanyId = company.Id;
                }
            }else
            {
                var company = _сompanyRepository.GetById(user.CompanyId.Value);
                company.Name = string.IsNullOrEmpty(model.CompanyName) ? company.Name : model.CompanyName;
                company.Website = string.IsNullOrEmpty(model.CompanyUrl) ? company.Website : model.CompanyUrl;
                _сompanyRepository.Update(company);
            }
        }

        private string HashPassword(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }
        private bool VerifyPassword(User user, string hashedPassword, string confirmedPassword)
        {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, hashedPassword, confirmedPassword);
            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.Success:
                case PasswordVerificationResult.SuccessRehashNeeded:
                    return true;
            }
            return false;
        }

        #endregion
    }
}
