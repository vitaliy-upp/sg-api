using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.PaymentIntentCreated)]
    public class PaymentIntentCreatedEventHandler : BaseStripeEventHandler<PaymentIntent>
    {
        public PaymentIntentCreatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
