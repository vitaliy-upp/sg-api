using Stripe;

namespace PaymentStripe.Interfaces
{
    public interface IStripeEventHandlerFacroty
    {
        IBaseStripeEventHandler Create(Event stripeEvent);
    }
}
