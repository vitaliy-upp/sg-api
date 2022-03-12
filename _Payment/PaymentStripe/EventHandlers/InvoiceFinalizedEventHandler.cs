using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.InvoiceFinalized)]
    public class InvoiceFinalizedEventHandler : BaseStripeEventHandler<Invoice>
    {
        public InvoiceFinalizedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
