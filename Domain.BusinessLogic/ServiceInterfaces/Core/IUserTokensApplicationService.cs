using Domain.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IUserTokensApplicationService : IBaseBusinessService
    {
        /// <summary>
        /// Create email verification token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserTokenDto> CreateEmailVerificationTokenAsync(int userId, int expiredInDays = 1);

        /// <summary>
        /// Create invite token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserTokenDto CreateResetPasswordToken(int userId, int expiredInDays = 1);
        /// <summary>
        /// Find user token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserTokenDto> FindAsync(string token);
    }
}
