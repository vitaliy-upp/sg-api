using System.Collections.Generic;
using NoLimitTech.Application.Models;

namespace NoLimitTech.Application.ServiceInterfaces
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
        Domain.Models.UserCustomer GetUserCustomer();
        Domain.Models.StripeProductInfo GetStripeProdInfo(int planId);
    }
}
