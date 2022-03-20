using DataAccess.UserManagement;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IUserRepository : IDomainRepository<User, int>
    {
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Set role of specified user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        Task SetUserRoleAsync(int userId, int roleId);

        /// <summary>
        /// Get User with includes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetDetailedByIdAsync(int id);

        /// <summary>
        /// Get User with includes by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetDetailedByEmailAsync(string email);

        /// <summary>
        /// Check if the user is registered 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if available</returns>
        Task<bool> CheckEmailAvailabilityAsync(string email);
    }
}
