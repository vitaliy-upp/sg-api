using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.Models;
using Payment.DataAccess;

namespace Domain.DataAccess
{
    public interface IDomainDbContext : IUserManagementDbContext, IPaymentDbContext
    {
        DbSet<Message> ChatMessages { get; set; }
        DbSet<MessageAttachment> MessageAttachments { get; set; }
        DbSet<UserCustomer> UserCustomers { get; set; }
        DbSet<StripeProductInfo> StripeProductsInfo { get; set; }
    }
}
