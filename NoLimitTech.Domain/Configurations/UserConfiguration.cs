using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoLimitTech.Domain.Models;
using System;

namespace NoLimitTech.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasData(
            //    new User
            //    {
            //        Id = 1,
            //        FirstName = "Sergii",
            //        LastName = "K",
            //        Email = "sergii.k@upplabs.com",
            //        Password = "AQAAAAEAACcQAAAAEEt58dyo3aJZZTq2mMNb+nmTyJFhaCXQr46pyPdo2oz00rvn9YtK2HrpJEgIoyuVMw==",
            //        IsEmailVerified = true,
            //        PhoneNumber = "1234567890",
            //        Description = "",
            //        Image = "",
            //        CompanyName = "UppLabs",
            //        RoleInCompany = "Engineer",
            //        CompanyId = null
            //    },
            //    new User
            //    {
            //        Id = 2,
            //        FirstName = "Vitaliy",
            //        LastName = "D",
            //        Email = "vitaliy@Upplabs.com",
            //        Password = "AQAAAAEAACcQAAAAEFidLzNJWPYPVjHlWVc9rIRRvMbJqnQ1vqw845IAAK89pNOBHacikFp6Xo73kTJt8w==",
            //        IsEmailVerified = true,
            //        PhoneNumber = "1234567891",
            //        Description = "",
            //        Image = "",
            //        CompanyName = "UppLabs",
            //        RoleInCompany = "CEO",
            //        CompanyId = null
            //    }
            //    );
        }
    }
}
