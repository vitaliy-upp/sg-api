using DataAccess.UserManagement;
using Microsoft.AspNetCore.Http;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using Domain.DataAccess.Services;
using PaymentStripe.Attributes;
using Stripe;
using System;
using System.Threading.Tasks;

namespace PaymentStripe.EventHandlers
{
    [EventType(Type = Events.CustomerSubscriptionUpdated)]
    public class CustomerSubscriptionUpdatedEventHandler : BaseStripeEventHandler<Subscription>
    {
        private readonly IUserDomainService _userDomainService;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IUserCustomerRepository _userCustomerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IStripeProductInfoRepository _stripeProductInfoRepository;

        public CustomerSubscriptionUpdatedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor)
            : base(stripeEvent, httpContextAccessor)
        {
            IServiceProvider serviceProvider = HttpContextAccessor.HttpContext.RequestServices;
            _userDomainService = serviceProvider.GetService(typeof(IUserDomainService)) as IUserDomainService;
            _subscriptionPlanRepository = serviceProvider.GetService(typeof(ISubscriptionPlanRepository)) as ISubscriptionPlanRepository;
            _userCustomerRepository = serviceProvider.GetService(typeof(IUserCustomerRepository)) as IUserCustomerRepository;
            _companyRepository = serviceProvider.GetService(typeof(ICompanyRepository)) as ICompanyRepository;
            _stripeProductInfoRepository = serviceProvider.GetService(typeof(IStripeProductInfoRepository)) as IStripeProductInfoRepository;
        }

        public override async Task HandleAsync()
        {
            await base.HandleAsync();

            UserCustomer userCustomer = _userCustomerRepository.GetByCustomerId(DataObj.CustomerId);
            User user = _userDomainService.GetDetailedById(userCustomer.UserId);
            StripeProductInfo stripeProductInfo = _stripeProductInfoRepository.GetByPriceId(DataObj.Items.Data[0].Price.Id);
            //SubscriptionPlan subPlan = _subscriptionPlanRepository.GetById(stripeProductInfo.SubscriptionPlanId.Value);
            //user.Company.SubscriptionPlanId = subPlan.Id;
            //_companyRepository.Update(user.Company);
        }
    }
}
