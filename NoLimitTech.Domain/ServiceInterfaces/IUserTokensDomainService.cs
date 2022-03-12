using DataAccess.UserManagement;
using NoLimitTech.Domain.Models;

namespace NoLimitTech.Domain.ServiceInterfaces
{
    public interface IUserTokensDomainService : IDomainService<UserToken, int>
    {
        /// <summary>
        /// Find user token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserToken Find(string token);
    }
}
