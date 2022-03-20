using DataAccess.UserManagement;
using Domain.DataAccess.Models;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IUserTokensDomainService : IDomainRepository<UserToken, int>
    {
        /// <summary>
        /// Find user token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserToken> FindAsync(string token);
    }
}
