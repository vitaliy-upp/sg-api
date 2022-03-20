using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IUserService : IBaseBusinessService
    {
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetByIdAsync(int id);

        /// <summary>
        /// Find User by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserDto> GetByEmailAsync(string email);

        /// <summary>
        /// Find user with detailes by identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<User> GetDetailedByIdentityAsync(IIdentity identity);

        /// <summary>
        /// Get user profile by claims identity (by email)
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        Task<UserDto> GetUserByIdentityAsync(IIdentity identity);

        /// <summary>
        /// Find User by Login and Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User model</returns>
        Task<UserDto> GetByLoginPasswordAsync(string username, string password);

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User model</returns>
        Task<UserDto> CreateAsync(RegistrationModel user);
        /// <summary>
        /// Create new User by invite key
        /// </summary>
        /// <param name="email"></param>
        /// <param name="reguser"></param>
        /// <returns></returns>
        Task<UserDto> CreateAsync(string email, URegistrationModel reguser);

        /// <summary>
        /// Check whether user already exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if it available, false if it is not</returns>
        Task<bool> CheckEmailAvailabilityAsync(string email);

        /// <summary>
        /// Confirm an email
        /// </summary>
        /// <param name="id"></param>
        Task ConfirmEmailAsync(int id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updateModel"></param>
        Task<UserDto> UpdateAsync(string userEmail, UpdateUserModel model);

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        Task ChangePasswordAsync(int userId, string oldPassword, string newPassword);

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        Task ChangePasswordAsync(int userId, string newPassword);

        /// <summary>
        /// Update user image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageName"></param>
        Task UpdateImageAsync(int userId, string imageName);
    }
}
