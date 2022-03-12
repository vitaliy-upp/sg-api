using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoLimitTech.Domain.Configurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            //builder.HasData(
            //    new UserRoles { UserId = 1, RoleId = 1 },
            //    new UserRoles { UserId = 2, RoleId = 1 }
            //    );
        }
    }
}
