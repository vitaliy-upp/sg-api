using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Payment.DataAccess.Enitities;

namespace DataAccess
{
    public interface IUserManagementDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
        DbSet<Company> Companies { get; set; }
    }
}
