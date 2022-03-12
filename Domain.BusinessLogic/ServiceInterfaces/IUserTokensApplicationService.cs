using Domain.BusinessLogic.Models;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IUserTokensApplicationService : IApplicationService
    {
        /// <summary>
        /// Create email verification token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserTokenModel CreateEmailVerificationToken(int userId, int expiredInDays = 1);
        /// <summary>
        /// Create invite token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserTokenModel CreateResetPasswordToken(int userId, int expiredInDays = 1);
        /// <summary>
        /// Find user token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserTokenModel Find(string token);
    }
}
