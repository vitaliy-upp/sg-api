using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using Stripe;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Stripe.Events.CustomerCreated)]
    public class CustomerCreatedEventHandler : BaseStripeEventHandler<Customer>
    {
        public CustomerCreatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
            
        }
    }
}
