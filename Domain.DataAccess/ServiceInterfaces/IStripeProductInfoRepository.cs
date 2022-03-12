using Domain.DataAccess.Models;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IStripeProductInfoRepository : IDomainService<StripeProductInfo, int>
    {
        /// <summary>
        /// Get StripeProductInfo by event id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StripeProductInfo GetByEventId(int id);

        /// <summary>
        /// Get StripeProductInfo by subscription plan id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StripeProductInfo GetByPlanId(int id);

        /// <summary>
        /// Get StripeProductInfo by price id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StripeProductInfo GetByPriceId(string id);

        /// <summary>
        /// Get StripeProductInfo by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StripeProductInfo GetByProductId(string id);
    }
}
