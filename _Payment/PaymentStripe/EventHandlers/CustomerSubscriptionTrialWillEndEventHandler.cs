using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.CustomerSubscriptionTrialWillEnd)]
    public class CustomerSubscriptionTrialWillEndEventHandler : BaseStripeEventHandler<Subscription>
    {
        public CustomerSubscriptionTrialWillEndEventHandler(Event stripeEvent, HttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }

    }
}
