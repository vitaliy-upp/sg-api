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
using System.Collections.Generic;
using FileManagement.DataAccess;

namespace Domain.BusinessLogic.Services
{
    public class UserService : BaseBusinessService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAttachmentService _mediaApplicationService;
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly ICompanyRepository _сompanyRepository;
        private readonly IMapper _mapper;

        private readonly ImageSettings _imageSettings; // TODO: maybe move this to blob storage settings

        public UserService(IHttpContextAccessor httpContextAccessor
            , DataAccess.ServiceInterfaces.IUserRepository userDomainService
            , IConfiguration configuration
            , IAttachmentService mediaApplicationService
            , ISocialLinkRepository socialLinkRepository
            , IMapper mapper
            , ICompanyRepository companyRepository
            ) : base(httpContextAccessor)
        {
            _userRepository = userDomainService;
            _mediaApplicationService = mediaApplicationService;
            _socialLinkRepository = socialLinkRepository;
            _mapper = mapper;
            _сompanyRepository = companyRepository;
            _imageSettings = configuration.GetSection(nameof(ImageSettings)).Get<ImageSettings>();
        }


        public async Task<bool> CheckEmailAvailabilityAsync(string email)
        {
            return await _userRepository.CheckEmailAvailabilityAsync(email);
        }
        public async Task<UserDto> CreateAsync(RegistrationModel user)
        {
            var newuser = _mapper.Map<User>(user);
            return await CreateAsync(newuser, user);
        }
        public async Task<UserDto> CreateAsync(string email, URegistrationModel reguser)
        {
            var newuser = _mapper.Map<User>(reguser);
            newuser.Email = email;
            newuser.IsEmailVerified = true;
            var regModel = _mapper.Map<RegistrationModel>(reguser);
            return await CreateAsync(newuser, regModel);
        }
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task ConfirmEmailAsync(int id)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            dbUser.IsEmailVerified = true;
            await _userRepository.UpdateAsync(dbUser);
        }

        public async Task<UserDto> GetByLoginPasswordAsync(string username, string password)
        {
            var dbUser = await _userRepository.GetByEmailAsync(username);
            if (dbUser == null)
            {
                throw new Exception("Invalid login or password");
            }

            if (!dbUser.IsEmailVerified)
            {
                throw new Exception("Please verify your email");
            }

            var isValid = VerifyPassword(dbUser, dbUser.Password, password);
            if (!isValid)
            {
                throw new Exception("Invalid login or password");
            }

            var model = _mapper.Map<UserDto>(dbUser);
            var user = await _userRepository.GetByIdAsync(model.Id);
            var role = user.UserRoles.FirstOrDefault();
            if (role != null)
            {
                model.Role = (UserRolesEnum)role.RoleId;
            }

            return model;
        }
        public async Task<UserDto> FindByIdentityAsync(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail)) return null;

            var dbUser = await _userRepository.GetByEmailAsync(userEmail);
            dbUser.Image = GetUserImage(dbUser.Image);
            return _mapper.Map<UserDto>(dbUser);
        }
        public async Task<User> GetDetailedByIdentityAsync(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail)) return null;

            var user = await _userRepository.GetDetailedByEmailAsync(userEmail);
            user.Image = GetUserImage(user.Image);
            return user;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var dbUser = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> GetUserByIdentityAsync(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userEmail = claimsIdentity.GetValueByClaimType(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return null;
            }
            var dbUser = await _userRepository.GetByEmailAsync(userEmail);
            //dbUser = await _userDomainService.GetDetailedByIdAsync(dbUser.Id);
            dbUser.Image = GetUserImage(dbUser.Image);
            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> UpdateAsync(string userEmail, UpdateUserModel model)
        {
            var dbUser = await _userRepository.GetByEmailAsync(userEmail);
            var user = _mapper.Map<User>(model);

            user.Id = dbUser.Id;
            user.IsEmailVerified = true;
            user.CompanyId = dbUser.CompanyId;

            if (model.Image == null)
            {
                user.Image = dbUser.Image;
            }
            else
            {
                // Adding image of new user
                user.Image = await _mediaApplicationService.UploadAsync(model.Image);
                //UpdateImage(dbUser.Id, imageName);
            }

            UpdateCompany(user, model);

            await _userRepository.UpdateAsync(user);

            CreateSocialLinksAsync(user.Id, model);

            var updatedUser = await _userRepository.GetDetailedByIdAsync(user.Id);
            var updatedUserModel = _mapper.Map<UserDto>(updatedUser);
            return updatedUserModel;
        }

        public async Task ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var dbUser = await _userRepository.GetByIdAsync(userId);

            var isVerified = VerifyPassword(dbUser, dbUser.Password, oldPassword);
            if (!isVerified)
            {
                throw new Exception("Password is not valid");
            }

            dbUser.Password = HashPassword(dbUser, newPassword);
            await _userRepository.UpdateAsync(dbUser);
        }
        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            var dbUser = await _userRepository.GetByIdAsync(userId);
            dbUser.Password = HashPassword(dbUser, newPassword);
            await _userRepository.UpdateAsync(dbUser);
        }
        public async Task UpdateImageAsync(int userId, string imageName)
        {
            var dbuser = await _userRepository.GetByIdAsync(userId);
            if (dbuser == null) return;

            dbuser.Image = imageName;
            await _userRepository.UpdateAsync(dbuser);
        }

        //public async  bool IsRegistered()
        //{
        //    return _userDomainService.CheckEmailAvailabilityAsync(CurrentUserEmail);
        //}


        #region private methods

        private string GetUserImage(string image)
        {
            if (image == null) return String.Empty;

            var img = image.Split("/").Last();
            return $"{_imageSettings.Path}/{img}";
        }
        private async Task<UserDto> CreateAsync(User user, RegistrationModel regUser)
        {
            // Getting a hash of password
            var passwdHash = HashPassword(user, regUser.Password);
            user.Password = passwdHash;
            
            if (regUser.ImageFile != null)
            {
                user.Image = await _mediaApplicationService.UploadAsync(regUser.ImageFile);
            }
            else
            {
                user.Image = "";
            }

            //if (!string.IsNullOrEmpty(regUser.CompanyName))
            //{
            //    var company = new Company()
            //    {
            //        Name = regUser.CompanyName,
            //        Website = regUser.CompanyUrl
            //    };
            //    company = _сompanyRepository.Create(company);
            //    user.CompanyId = company.Id;
            //}

            if (user.UserRoles == null)
            {
                user.UserRoles = new List<UserRoles>();
            }
            user.UserRoles.Add(new UserRoles() { 
                User = user,
                RoleId = (int) UserRolesEnum.User
            });
            var result = await _userRepository.CreateAsync(user, true);
            return _mapper.Map<UserDto>(result);
        }

        private async Task CreateSocialLinksAsync(int userId, UpdateUserModel updateUser)
        {
            if (updateUser.SocialLinks != null)
            {
                var links = _socialLinkRepository.GetForUser(userId);
                await _socialLinkRepository.DeleteAsync(links.Select(sl => sl.Id).ToList());
                var list = updateUser.SocialLinks.Select((sl) => new Attachment()
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
            }
            else
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
