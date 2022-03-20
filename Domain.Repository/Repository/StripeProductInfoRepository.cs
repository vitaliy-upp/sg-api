using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using System.Linq;

namespace Domain.DataAccess.Services
{
    public class StripeProductInfoRepository : DomainRepository<StripeProductInfo, int>, IStripeProductInfoRepository
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
