using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.InvoiceVoided)]
    public class InvoiceVoidedEventHandler : BaseStripeEventHandler<Invoice>
    {
        public InvoiceVoidedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
