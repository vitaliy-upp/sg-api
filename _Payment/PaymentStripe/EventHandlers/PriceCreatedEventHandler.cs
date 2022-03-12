using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.PriceCreated)]
    public class PriceCreatedEventHandler : BaseStripeEventHandler<Price>
    {
        public PriceCreatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
