using DataAccess.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NoLimitTech.Application.ServiceInterfaces;
using NoLimitTech.Application.Settings;
using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;
using NoLimitTech.Domain.Services;
using PaymentStripe.Interfaces;
using PaymentStripe.Settings;
using PaymentStripe.Utils;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NoLimitTech.Application.Services
{
    public class StripeApplicationService : BaseApplicationService, IStripeApplicationService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly ILogger<StripeApplicationService> _logger;
        private readonly IUserCustomerRepository _userCustomerRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IStripeEventHandlerFacroty _stripeEventHandlerFacroty;
        private readonly IStripeProductInfoRepository _stripeProductInfoRepository;
        private readonly ICompanyRepository _companyRepository;

        private readonly StripeSettings _stripeSettings;
        private readonly StripeOutcomeUrlSettings _stripeOutcomeUrlSettings;
        private readonly ClientSideSettings _clientSideSettings;

        public StripeApplicationService(IConfiguration configuration
            , IHttpContextAccessor httpContextAccessor
            , IUserDomainService userDomainService
            , ILogger<StripeApplicationService> logger
            , IUserCustomerRepository userCustomerRepository
            , ISubscriptionPlanRepository subscriptionPlanRepository
            , IStripeEventHandlerFacroty stripeEventHandlerFacroty
            , IStripeProductInfoRepository stripeProductInfoRepository
            , ICompanyRepository companyRepository
            ) : base(httpContextAccessor)
        {
            _userDomainService = userDomainService;
            _logger = logger;
            _userCustomerRepository = userCustomerRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _stripeEventHandlerFacroty = stripeEventHandlerFacroty;
            _stripeProductInfoRepository = stripeProductInfoRepository;
            _companyRepository = companyRepository;

            _stripeSettings = configuration.GetSection(nameof(StripeSettings)).Get<StripeSettings>();
            _stripeOutcomeUrlSettings = configuration.GetSection(nameof(StripeOutcomeUrlSettings)).Get<StripeOutcomeUrlSettings>();
            _clientSideSettings = configuration.GetSection(nameof(ClientSideSettings)).Get<ClientSideSettings>();

            StripeConfiguration.ApiKey = _stripeSettings.SecretApiKey;
        }

        public async Task<object> CreateCheckoutSessionForPlan(int planId)
        {
            User user = _userDomainService.GetDetailedByEmail(CurrentUserEmail);
            if (user.Company == null)
            { throw new Exception("You do not have company"); }

            UserCustomer ucustomer = _userCustomerRepository.GetByUserId(user.Id);
            if (ucustomer == null)
            {
                Customer newStripeCustomer = await CreateStripeCustomerAsync(user);
                ucustomer = _userCustomerRepository.Create(user.Id, newStripeCustomer.Id);
            }
            Customer stripeCustomer = await GetStripeCustomerAsync(ucustomer.CustomerId, new[] { "subscriptions" });
            if (stripeCustomer.Subscriptions != null && stripeCustomer.Subscriptions.Data.Count > 0)
            {
                var session = await CreateStripeCustomerSessionAsync(ucustomer.CustomerId);
                return new { url = session.Url };
            }
            else
            {
                var strProductInfo = _stripeProductInfoRepository.GetByPlanId(planId);
                Subscription subscription = CreateStripeCustomerSubscription(ucustomer.CustomerId, strProductInfo.PriceId);
                var stripeProductInfo = _stripeProductInfoRepository.GetByPriceId(subscription.Items.Data[0].Price.Id);
                if (stripeProductInfo != null)
                {
                    user.Company.SubscriptionPlanId = stripeProductInfo.SubscriptionPlanId;
                    //_companyRepository.Update(user.Company);
                }
                //var session = await CreateStripeCheckoutSessionAsync(strProductInfo, ucustomer.CustomerId, null);
                return new
                {
                    subscriptionId = user.Company.SubscriptionPlanId,
                    trialPeriod = true
                };
            }
        }
        public async Task<string> CreateCheckoutSessionForTicket(int eventId, User user)
        {
            var ucustomer = _userCustomerRepository.GetByUserId(user.Id);
            if (ucustomer == null)
            {
                Customer customer = await CreateStripeCustomerAsync(user);
                ucustomer = _userCustomerRepository.Create(user.Id, customer.Id);
            }
            var strProductInfo = _stripeProductInfoRepository.GetByEventId(eventId);
            var session = await CreateStripeCheckoutSessionAsync(strProductInfo, ucustomer.CustomerId, eventId);
            return session.Id;
        }

        public async Task<string> CreateCustomerSessionAsync()
        {
            User user = _userDomainService.GetByEmail(CurrentUserEmail);
            var ucustomer = _userCustomerRepository.GetByUserId(user.Id);
            Customer customer = null;
            if (ucustomer == null)
            {
                customer = await CreateStripeCustomerAsync(user);
                _userCustomerRepository.Create(user.Id, customer.Id);
            }
            else
            {
                customer = await GetStripeCustomerAsync(ucustomer.CustomerId);
            }
            var session = await CreateStripeCustomerSessionAsync(customer.Id);
            return session.Url;
        }

        public async Task HandleWebHookAsync()
        {
            var json = await new StreamReader(HttpContextAccessor.HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ParseEvent(json);

            var eh = _stripeEventHandlerFacroty.Create(stripeEvent);
            await eh.HandleAsync();
        }

        public async Task CreateProductAsync()
        {

            throw new NotImplementedException();
        }

        public async Task UpdateOrCreateProductAsync()
        {
            throw new NotImplementedException();
        }

        public StripeProductInfo FindStripeProductInfoByEventId(int id)
        {
            return _stripeProductInfoRepository.GetByEventId(id);
        }

        public Subscription CreateStripeCustomerSubscription(string customerId, string priceId)
        {
            var items = new List<SubscriptionItemOptions> {
                new SubscriptionItemOptions { Price = priceId }
            };
            var options = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = items,
                TrialPeriodDays = 30
            };
            var service = new SubscriptionService();
            return service.Create(options);
        }

        #region PRIVATE METHODS

        private async Task<Stripe.Checkout.Session> CreateStripeCheckoutSessionAsync(StripeProductInfo stripeProductInfo, string customerId, int? eventId)
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>()
                {
                    new SessionLineItemOptions
                    {
                        Price = stripeProductInfo.PriceId,
                        Quantity = 1
                    },
                },
                Mode = stripeProductInfo.SubscriptionPlanId.HasValue ? "subscription" : "payment",
                Customer = customerId
            };
            if (stripeProductInfo.SubscriptionPlanId.HasValue)
            {
                options.SuccessUrl = $"{_clientSideSettings.Url}/{_stripeOutcomeUrlSettings.SubscriptionPaymentSuccess}";
                options.CancelUrl = $"{_clientSideSettings.Url}/{_stripeOutcomeUrlSettings.SubscriptionPaymentCancel}";
            }
            else
            {
                options.SuccessUrl = $"{_clientSideSettings.Url}/{_stripeOutcomeUrlSettings.TicketPaymentSuccess}";
                options.CancelUrl = $"{_clientSideSettings.Url}/{_stripeOutcomeUrlSettings.GetTicketPaymentCancelUrl(eventId.Value)}";
            }
            var service = new Stripe.Checkout.SessionService();
            return await service.CreateAsync(options);
        }

        private async Task<Customer> CreateStripeCustomerAsync(User user)
        {
            var options = new CustomerCreateOptions
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };
            var service = new CustomerService();
            return await service.CreateAsync(options);
        }

        private async Task<Customer> GetStripeCustomerAsync(string id, IEnumerable<string> expandedList = null)
        {
            CustomerGetOptions options = null;
            if (expandedList != null)
            {
                options = new CustomerGetOptions();
                options.AddRangeExpand(expandedList);
            }
            var service = new CustomerService();
            return await service.GetAsync(id, options);
        }

        private async Task<Stripe.BillingPortal.Session> CreateStripeCustomerSessionAsync(string customerId)
        {
            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = customerId,
                ReturnUrl = $"{_clientSideSettings.Url}/{_stripeOutcomeUrlSettings.SubscriptionPaymentSuccess}"
            };
            var service = new Stripe.BillingPortal.SessionService();
            return await service.CreateAsync(options);
        }

        private async Task<Product> CreateStripeProductAsync(string title)
        {
            var options = new ProductCreateOptions
            {
                Name = $"Ticket to {title}",
                Active = true,
                Type = "service",
                Description = $"Ticket to {title}"
            };
            var service = new ProductService();
            return await service.CreateAsync(options);
        }

        private async Task<Price> CreateStripePriceAsync(long price, string productId)
        {
            var options = new PriceCreateOptions
            {
                UnitAmount = price,
                Currency = "usd",
                Product = productId,
            };
            var service = new PriceService();
            return await service.CreateAsync(options);
        }

        private async Task<Product> UpdateStripeProductAsync(string id)
        {
            var options = new ProductUpdateOptions
            {
                Name = $"Ticket to ",
                Active = true,
                Description = $"Ticket to "
            };
            var service = new ProductService();
            return await service.UpdateAsync(id, options);
        }

        #endregion
    }
}
