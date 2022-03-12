using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;
using System.Linq;

namespace NoLimitTech.Domain.Services
{
    public class StripeProductInfoRepository : DomainService<StripeProductInfo, int>, IStripeProductInfoRepository
    {
        public StripeProductInfoRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

        public StripeProductInfo GetByEventId(int id)
        {
            return Context.Set<StripeProductInfo>()
                .FirstOrDefault(x => x.ConferenceId == id);
        }

        public StripeProductInfo GetByPlanId(int id)
        {
            return Context.Set<StripeProductInfo>()
                .FirstOrDefault(x => x.SubscriptionPlanId == id);
        }

        public StripeProductInfo GetByPriceId(string id)
        {
            return Context.Set<StripeProductInfo>()
                .FirstOrDefault(x => x.PriceId == id);
        }

        public StripeProductInfo GetByProductId(string id)
        {
            return Context.Set<StripeProductInfo>()
                .FirstOrDefault(x => x.ProductId == id);
        }
    }
}
