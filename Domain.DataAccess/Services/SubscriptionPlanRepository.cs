using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.ServiceInterfaces;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.Services
{
    public class SubscriptionPlanRepository : DomainService<SubscriptionPlan, int>, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public override IList<SubscriptionPlan> GetAll()
        {
            return Context.Set<SubscriptionPlan>().Include(x => x.SubscriptionFeatures).ThenInclude(f => f.Feature).ToList();
        }

        public SubscriptionPlan GetByPrice(decimal price)
        {
            return Context.Set<SubscriptionPlan>()
                .FirstOrDefault(x => x.Price == price);
        }

        public SubscriptionPlan GetByEventId(int id)
        {
            return null;
        }
    }
}
