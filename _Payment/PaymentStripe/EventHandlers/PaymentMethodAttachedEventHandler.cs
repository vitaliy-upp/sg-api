using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.PaymentMethodAttached)]
    public class PaymentMethodAttachedEventHandler : BaseStripeEventHandler<PaymentMethod>
    {
        public PaymentMethodAttachedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
