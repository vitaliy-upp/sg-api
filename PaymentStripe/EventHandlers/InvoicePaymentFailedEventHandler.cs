using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.InvoicePaymentFailed)]
    public class InvoicePaymentFailedEventHandler : BaseStripeEventHandler<Invoice>
    {
        public InvoicePaymentFailedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
