using AutoMapper;
using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System;
using System.Threading.Tasks;

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

        public async Task<UserTokenDto> CreateEmailVerificationTokenAsync(int userId, int expiredInDays = 1)
        {
            UserToken userToken = new UserToken()
            {
                UserId = userId,
                ExpiredDate = DateTime.UtcNow.AddDays(expiredInDays),
                UserTokenType = UserTokenTypeEnum.EmailVerification,
            };
            userToken = await _userTokensDomainService.CreateAsync(userToken);
            return _mapper.Map<UserTokenDto>(userToken);
        }

        public UserTokenDto CreateResetPasswordToken(int userId, int expiredInDays = 1)
        {
            UserToken userToken = new UserToken()
            {
                UserId = userId,
                ExpiredDate = DateTime.UtcNow.AddDays(expiredInDays),
                UserTokenType = UserTokenTypeEnum.ResetPassword
            };
            userToken = _userTokensDomainService.Create(userToken);
            return _mapper.Map<UserTokenDto>(userToken);
        }

        public async Task<UserTokenDto> FindAsync(string token)
        {
            UserToken dbUserToken = await _userTokensDomainService.FindAsync(token);
            return _mapper.Map<UserTokenDto>(dbUserToken);
        }
    }
}
