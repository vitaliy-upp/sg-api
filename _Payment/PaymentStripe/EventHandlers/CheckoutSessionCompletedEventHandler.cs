using DataAccess.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Domain.DataAccess.ServiceInterfaces;
using Domain.DataAccess.Services;
using Payment.DataAccess.Enitities;
using PaymentStripe.Attributes;
using PaymentStripe.Utils;
using Stripe;
using Stripe.Checkout;
using System;
using System.Threading.Tasks;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Events.CheckoutSessionCompleted)]
    public class CheckoutSessionCompletedEventHandler : BaseStripeEventHandler<Session>
    {
        private readonly IUserCustomerRepository _userCustomerRepository;
        private readonly IStripeProductInfoRepository _stripeProductInfoRepository;
        private readonly IUserRepository _userDomainService;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly ICompanyRepository _companyRepository;

        public CheckoutSessionCompletedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor) 
            : base(stripeEvent, httpContextAccessor)
        {
            IServiceProvider serviceProvider = HttpContextAccessor.HttpContext.RequestServices;
            _userCustomerRepository = serviceProvider.GetService(typeof(IUserCustomerRepository)) as IUserCustomerRepository;
            _stripeProductInfoRepository = serviceProvider.GetService(typeof(IStripeProductInfoRepository)) as IStripeProductInfoRepository;
            _userDomainService = serviceProvider.GetService(typeof(IUserRepository)) as IUserRepository;
            _subscriptionPlanRepository = serviceProvider.GetService(typeof(ISubscriptionPlanRepository)) as ISubscriptionPlanRepository;
            _companyRepository = serviceProvider.GetService(typeof(ICompanyRepository)) as ICompanyRepository;
        }

        public override async Task HandleAsync()
        {
            await base.HandleAsync();

            if (DataObj.PaymentStatus != "paid")
            {
                Logger.LogError($"Invoice was not paid. Customer: {DataObj.CustomerId}");
                return;
            }

            // getting expanded checkout session object
            SessionGetOptions sessionOptions = new SessionGetOptions();
            sessionOptions.AddExpand("line_items");
            var stripeCheckoutSession = await new SessionService().GetAsync(DataObj.Id, sessionOptions);
            // find out what product was purchaiced by getting price id
            string priceId = stripeCheckoutSession.LineItems.Data[0].Price.Id;

            var ucustomer = _userCustomerRepository.GetByCustomerId(stripeCheckoutSession.CustomerId);
            
            var stripeProductInfo = _stripeProductInfoRepository.GetByPriceId(priceId);
            if (stripeProductInfo.ConferenceId.HasValue)
            {
                decimal amountPaid = stripeCheckoutSession.LineItems.Data[0].AmountTotal.HasValue
                    ? StripeUtils.UnitsToPrice(stripeCheckoutSession.LineItems.Data[0].AmountTotal.Value)
                    : 0;
                ProvideTicket(ucustomer.UserId, stripeProductInfo.ConferenceId.Value, amountPaid);
            }
            else
            {
                ProvideSubscriptionAsync(ucustomer.UserId, stripeProductInfo.SubscriptionPlanId.Value);
            }
        }


        #region PRIVATE METHODS

        
        private async Task ProvideSubscriptionAsync(int userId, int planId)
        {
            User user = await _userDomainService.GetDetailedByIdAsync(userId);
            SubscriptionPlan subPlan = await _subscriptionPlanRepository.GetByIdAsync(planId);
            user.Company.SubscriptionPlanId = subPlan.Id;
            //_companyRepository.Update(user.Company);
        }

        private void ProvideTicket(int userId, int eventId, decimal amountPaid)
        {
           throw new NotImplementedException();
        }

        #endregion
    }
}
