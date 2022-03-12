using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoLimitTech.Application.Models;
using NoLimitTech.Application.ServiceInterfaces;
using NoLimitTech.Domain.ServiceInterfaces;
using NoLimitTech.Domain.Services;

namespace NoLimitTech.Application.Services
{
    public class SubscriptionPlanApplicationService : BaseApplicationService, ISubscriptionPlanApplicationService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly ICompanyRepository _сompanyRepository;
        private readonly IUserDomainService _userDomainService;
        private readonly IMapper _mapper;

        private readonly IUserCustomerRepository _userCustomerRepository;
        private readonly IStripeProductInfoRepository _stripeProductInfoRepository;

        public SubscriptionPlanApplicationService(IHttpContextAccessor httpContextAccessor
            , ISubscriptionPlanRepository subscriptionPlanRepository
            , ICompanyRepository сompanyRepository
            , IUserDomainService userDomainService
            , IMapper mapper
            , IUserCustomerRepository userCustomerRepository
            , IStripeProductInfoRepository stripeProductInfoRepository
            )
            : base(httpContextAccessor)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _сompanyRepository = сompanyRepository;
            _userDomainService = userDomainService;
            _mapper = mapper;
            _userCustomerRepository = userCustomerRepository;
            _stripeProductInfoRepository = stripeProductInfoRepository;
        }

        public GetSubscriptionPlansDTO GetSubscriptionPlans()
        {
            var user = _userDomainService.GetDetailedByEmail(CurrentUserEmail);
            var plansDb = _subscriptionPlanRepository.GetAll();
            GetSubscriptionPlansDTO dto = new GetSubscriptionPlansDTO()
            {
                CurrentPlanId = user.Company?.SubscriptionPlanId,
                Subscriptions = _mapper.Map<List<SubscriptionPlanDTO>>(plansDb)
            };
            return dto;
        }

        public void UpdateCompanySubscription(UpdateCompanySubscriptionModel updateCompanySubscriptionModel, int userId)
        {
            //var user = _userDomainService.GetById(userId);
            //var company = _сompanyRepository.GetById(user.CompanyId.Value);            
            //company.SubscriptionPlanId = updateCompanySubscriptionModel.SubscriptionPlanId;
            //_сompanyRepository.Update(company);
            //_сompanyRepository.SaveChanges();
        }

        public void RemoveCompanySubscription(int userId)
        {
            //var user = _userDomainService.GetById(userId);
            //var company = _сompanyRepository.GetById(user.CompanyId.Value);
            //company.SubscriptionPlanId = null;
            //_сompanyRepository.Update(company);
            //_сompanyRepository.SaveChanges();
        }

        // For Testing
        public SubscriptionPlanDTO FindById(int id)
        {
            var dbSubscr = _subscriptionPlanRepository.GetById(id);
            return _mapper.Map<SubscriptionPlanDTO>(dbSubscr);
        }
        public Domain.Models.StripeProductInfo GetStripeProdInfo(int planId)
        {
            return _stripeProductInfoRepository.GetByPlanId(planId);
        }
        public Domain.Models.UserCustomer GetUserCustomer()
        {
            var user = _userDomainService.GetByEmail(CurrentUserEmail);
            return _userCustomerRepository.GetByUserId(user.Id);
        }
    }
}
