using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.CustomerSubscriptionCreated)]
    public class CustomerSubscriptionCreatedEventHandler : BaseStripeEventHandler<Subscription>
    {
        public CustomerSubscriptionCreatedEventHandler(Event stripeEvent, HttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }

    }
}
