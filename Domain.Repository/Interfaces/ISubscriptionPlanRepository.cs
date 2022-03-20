using Domain.Repository;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface ISubscriptionPlanRepository : IDomainRepository<SubscriptionPlan, int>
    {
        /// <summary>
        /// Get Subscription plan by price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        SubscriptionPlan GetByPrice(decimal price);

        /// <summary>
        /// Get Subscription Plan by event id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SubscriptionPlan GetByEventId(int id);
    }
}
