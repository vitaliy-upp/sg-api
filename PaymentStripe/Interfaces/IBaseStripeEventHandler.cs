using System.Threading.Tasks;

namespace PaymentStripe.Interfaces
{
    public interface IBaseStripeEventHandler
    {
        Task HandleAsync();
    }
}
