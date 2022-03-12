using AutoMapper;
using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System;

namespace Domain.BusinessLogic.Services
{
    public class UserTokensApplicationService : IUserTokensApplicationService
    {
        private readonly IUserTokensDomainService _userTokensDomainService;
        private readonly IMapper _mapper;

        public UserTokensApplicationService(IUserTokensDomainService userTokensDomainService
            , IMapper mapper)
        {
            _userTokensDomainService = userTokensDomainService;
            _mapper = mapper;
        }

        public UserTokenModel CreateEmailVerificationToken(int userId, int expiredInDays = 1)
        {
            UserToken userToken = new UserToken()
            {
                UserId = userId,
                ExpiredDate = DateTime.UtcNow.AddDays(expiredInDays),
                UserTokenType = UserTokenTypeEnum.EmailVerification,
            };
            userToken = _userTokensDomainService.Create(userToken);
            return _mapper.Map<UserTokenModel>(userToken);
        }

        public UserTokenModel CreateResetPasswordToken(int userId, int expiredInDays = 1)
        {
            UserToken userToken = new UserToken()
            {
                UserId = userId,
                ExpiredDate = DateTime.UtcNow.AddDays(expiredInDays),
                UserTokenType = UserTokenTypeEnum.ResetPassword
            };
            userToken = _userTokensDomainService.Create(userToken);
            return _mapper.Map<UserTokenModel>(userToken);
        }

        public UserTokenModel Find(string token)
        {
            UserToken dbUserToken = _userTokensDomainService.Find(token);
            return _mapper.Map<UserTokenModel>(dbUserToken);
        }
    }
}
