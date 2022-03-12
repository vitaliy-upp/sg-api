using DataAccess.UserManagement;
using Domain.DataAccess.Models;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IStripeApplicationService
    {
        /// <summary>
        /// Create customer session for customer portal
        /// </summary>
        /// <returns></returns>
        Task<string> CreateCustomerSessionAsync();

        /// <summary>
        /// Handle Stripe Web Hook
        /// </summary>
        /// <returns></returns>
        Task HandleWebHookAsync();

        /// <summary>
        /// Create Stripe product
        /// </summary>
        /// <param name="conference"></param>
        /// <returns></returns>
        Task CreateProductAsync();

        /// <summary>
        /// Update Stripe Product,
        /// </summary>
        /// <param name="conference"></param>
        /// <returns></returns>
        Task UpdateOrCreateProductAsync();

        /// <summary>
        /// Create checkout session for subscription plan
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        Task<object> CreateCheckoutSessionForPlan(int planId);

        /// <summary>
        /// Create checkout session for ticket
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        Task<string> CreateCheckoutSessionForTicket(int eventId, User user);

        /// <summary>
        /// Find stripe product info by event id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StripeProductInfo FindStripeProductInfoByEventId(int id);

        // For Testing
        Stripe.Subscription CreateStripeCustomerSubscription(string customerId, string priceId);
    }
}
