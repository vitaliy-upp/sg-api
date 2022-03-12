using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Events.InvoicePaid)]
    public class InvoicePaidEventHandler : BaseStripeEventHandler<Invoice>
    {
        public InvoicePaidEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
        }

    }
}
