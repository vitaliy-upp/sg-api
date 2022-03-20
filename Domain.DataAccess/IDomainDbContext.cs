using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.Models;
using Payment.DataAccess;
using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Entities.KidProfile.Education;
using UserManagement.DataAccess.UserManagement.Location;

namespace Domain.DataAccess
{
    public interface IDomainDbContext : IUserManagementDbContext, IPaymentDbContext
    {
        #region KidProfile
        DbSet<KidProfile> KidProfiles { get; set; }
        DbSet<SuperPower> SuperPowers { get; set; }
        DbSet<Education> Educations { get; set; }
        #endregion


        #region Location
        DbSet<City> Cities { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<Country> Countries { get; set; }
        #endregion


        DbSet<Message> ChatMessages { get; set; }
        DbSet<MessageAttachment> MessageAttachments { get; set; }
        DbSet<UserCustomer> UserCustomers { get; set; }
        DbSet<StripeProductInfo> StripeProductsInfo { get; set; }
    }
}
