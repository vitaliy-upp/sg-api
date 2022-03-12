using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.CustomerSourceCreated)]
    public class CustomerSourceCreatedEventHandler : BaseStripeEventHandler<Card>
    {
        public CustomerSourceCreatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
