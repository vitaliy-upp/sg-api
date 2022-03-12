using Microsoft.EntityFrameworkCore;
using Payment.DataAccess.Enitities;

namespace Payment.DataAccess
{
    public interface IPaymentDbContext
    {
        DbSet<Feature> Features { get; set; }
        DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    }
}
