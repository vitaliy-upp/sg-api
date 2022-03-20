using System;
using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Enums;
using Domain.BusinessLogic.Extensions;
using Domain.BusinessLogic.ServiceInterfaces;
using System.Security.Claims;
using AutoMapper;

namespace Domain.BusinessLogic.Services
{
    public class BaseBusinessService: IBaseBusinessService
    {
        protected IHttpContextAccessor HttpContextAccessor;
        private string _currentUserEmail;
        private UserRolesEnum _currentUserRole = 0;
        public BaseBusinessService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUserEmail))
                { 
                    _currentUserEmail = (HttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).GetValueByClaimType(ClaimTypes.Email); 
                }
                return _currentUserEmail;
            }
        }

        public UserRolesEnum CurrentUserRole
        {
            get
            {
                if (_currentUserRole == 0)
                {
                    _currentUserRole = (UserRolesEnum)Enum.Parse(typeof(UserRolesEnum), (HttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).GetValueByClaimType(ClaimTypes.Role), true);
                }
                return _currentUserRole;
            }
        }

        public bool IsCurrentUserRegistered
        {
            get {
                return Convert.ToBoolean((HttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).GetValueByClaimType(AppConstants.IS_REGISTERED));
            }
        }

    }
}
