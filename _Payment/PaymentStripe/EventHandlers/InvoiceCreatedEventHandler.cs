using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.InvoiceCreated)]
    public class InvoiceCreatedEventHandler : BaseStripeEventHandler<Invoice>
    {
        public InvoiceCreatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }
    }
}
