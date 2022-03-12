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
    [EventType(Type = Events.CustomerSubscriptionDeleted)]
    public class CustomerSubscriptionDeletedEventHandler : BaseStripeEventHandler<Subscription>
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IUserCustomerRepository _userCustomerRepository;
        private readonly ICompanyRepository _companyRepository;

        public CustomerSubscriptionDeletedEventHandler(Event stripeEvent, IHttpContextAccessor httpContextAccessor) 
            : base(stripeEvent, httpContextAccessor)
        {
            IServiceProvider serviceProvider = HttpContextAccessor.HttpContext.RequestServices;
            _userDomainService = serviceProvider.GetService(typeof(IUserDomainService)) as IUserDomainService;
            _userCustomerRepository = serviceProvider.GetService(typeof(IUserCustomerRepository)) as IUserCustomerRepository;
            _companyRepository = serviceProvider.GetService(typeof(ICompanyRepository)) as ICompanyRepository;
        }


        public override async Task HandleAsync()
        {
            await base.HandleAsync();

            UserCustomer userCustomer = _userCustomerRepository.GetByCustomerId(DataObj.CustomerId);
            User user = _userDomainService.GetDetailedById(userCustomer.UserId);
            user.Company.SubscriptionPlanId = null;
            //_companyRepository.Update(user.Company);
        }
    }
}
