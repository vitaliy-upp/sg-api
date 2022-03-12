using DataAccess;
using Microsoft.EntityFrameworkCore;
using NoLimitTech.Domain.Models;
using Payment.DataAccess;

namespace NoLimitTech.Domain
{
    public interface IDomainDbContext : IUserManagementDbContext, IPaymentDbContext
    {
        DbSet<Message> ChatMessages { get; set; }
        DbSet<MessageAttachment> MessageAttachments { get; set; }
        DbSet<UserCustomer> UserCustomers { get; set; }
        DbSet<StripeProductInfo> StripeProductsInfo { get; set; }
    }
}
