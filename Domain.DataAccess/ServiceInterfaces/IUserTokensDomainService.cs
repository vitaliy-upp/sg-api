using DataAccess.UserManagement;
using Domain.DataAccess.Models;

namespace Domain.DataAccess.ServiceInterfaces
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
