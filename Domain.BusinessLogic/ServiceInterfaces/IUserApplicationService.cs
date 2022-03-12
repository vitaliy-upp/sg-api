using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IUserApplicationService : IApplicationService
    {
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel FindById(int id);

        /// <summary>
        /// Find User by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserModel FindByEmail(string email);

        /// <summary>
        /// Find user with detailes by identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        User FindDetailedByIdentity(IIdentity identity);

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User model</returns>
        Task<UserModel> CreateAsync(RegistrationModel user);
        /// <summary>
        /// Create new User by invite key
        /// </summary>
        /// <param name="email"></param>
        /// <param name="reguser"></param>
        /// <returns></returns>
        Task<UserModel> CreateAsync(string email, URegistrationModel reguser);

        /// <summary>
        /// Check whether user already exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if exists, false if not</returns>
        bool IfExists(string email);

        /// <summary>
        /// Confirm an email
        /// </summary>
        /// <param name="id"></param>
        void ConfirmEmail(int id);

        /// <summary>
        /// Find user by claims identity (by email)
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        UserModel FindByIdentity(IIdentity claimsIdentity);

        /// <summary>
        /// Get user profile by claims identity (by email)
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        UserModel GetUserProfileByIdentity(IIdentity identity);

        /// <summary>
        /// Find User by Login and Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User model</returns>
        UserModel FindByLoginPassword(string username, string password);
        
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updateModel"></param>
        Task<UserModel> UpdateAsync(string userEmail, UpdateUserModel model);

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        void ChangePassword(int userId, string oldPassword, string newPassword);

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        void ChangePassword(int userId, string newPassword);

        /// <summary>
        /// Update user image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageName"></param>
        void UpdateImage(int userId, string imageName);

        /// <summary>
        /// Check if the user is registered
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsRegistered();
    }
}
