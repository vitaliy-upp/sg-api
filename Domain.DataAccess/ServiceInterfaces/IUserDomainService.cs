using DataAccess.UserManagement;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IUserDomainService : IDomainService<User, int>
    {
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetByEmail(string email);

        /// <summary>
        /// Set role of specified user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        void SetUserRole(int userId, int roleId);

        /// <summary>
        /// Get user role id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetUserRoleId(int userId);

        /// <summary>
        /// Get User with includes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetDetailedById(int id);

        /// <summary>
        /// Get User with includes by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetDetailedByEmail(string email);

        /// <summary>
        /// Check if the user is registered 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsRegistered(string email);
    }
}
