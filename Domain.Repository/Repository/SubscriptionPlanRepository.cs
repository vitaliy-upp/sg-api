using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.ServiceInterfaces;
using Payment.DataAccess.Enitities;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class SubscriptionPlanRepository : DomainRepository<SubscriptionPlan, int>, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public override async Task<IList<SubscriptionPlan>> GetAllAsync()
        {
            return await Context.Set<SubscriptionPlan>()
                .Include(x => x.SubscriptionFeatures).ThenInclude(f => f.Feature)
                .ToListAsync();
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
