using Domain.BusinessLogic.Models;
using Domain.DataAccess.Models;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface ISubscriptionPlanApplicationService
    {
        /// <summary>
        /// Get all SubscriptionPlans
        /// </summary>
        /// <returns>List of SubscriptionPlan models</returns>
        GetSubscriptionPlansDTO GetSubscriptionPlans();

        /// <summary>
        /// Update SubscriptionPlan for specified company
        /// </summary>
        /// <returns></returns>
        void UpdateCompanySubscription(UpdateCompanySubscriptionModel updateCompanySubscriptionModel, int userId);

        /// <summary>
        /// Cancel subscription
        /// </summary>
        /// <returns></returns>
        void RemoveCompanySubscription(int userId);

        // For Testing
        SubscriptionPlanDTO FindById(int id);
        UserCustomer GetUserCustomer();
        StripeProductInfo GetStripeProdInfo(int planId);
    }
}
