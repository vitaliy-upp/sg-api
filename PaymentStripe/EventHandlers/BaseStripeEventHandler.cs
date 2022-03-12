using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentStripe.Interfaces;
using Stripe;
using System.Threading.Tasks;

namespace PaymentStripe.EventHandlers
{
    public class BaseStripeEventHandler<T> : IBaseStripeEventHandler
        where T : class
    {
        private Event StripeEvent { get; set; }
        public readonly ILogger<BaseStripeEventHandler<T>> Logger;

        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected T DataObj { get; set; }


        public BaseStripeEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
        {
            StripeEvent = stripeEvent;
            HttpContextAccessor = httpContextAccessor;
            Logger = HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(ILogger<BaseStripeEventHandler<T>>)) as ILogger<BaseStripeEventHandler<T>>;

            ParseDataObj(stripeEvent);
        }

        public virtual async Task HandleAsync()
        {
#if DEBUG
            await Task.Run(() =>
            {
                System.Console.WriteLine(" ====> Event: {0}", StripeEvent.Type);
                System.Console.WriteLine(" ====> Object: <[ {0} ]>", JsonConvert.SerializeObject(DataObj));
            });
#endif
        }


        private void ParseDataObj(Event stripeEvent)
        {
            DataObj = stripeEvent.Data.Object as T;
        }
    }


}
