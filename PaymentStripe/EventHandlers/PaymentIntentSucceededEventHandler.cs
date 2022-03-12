using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.PaymentIntentSucceeded)]
    public class PaymentIntentSucceededEventHandler : BaseStripeEventHandler<PaymentIntent>
    {
        public PaymentIntentSucceededEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }

    }
}
