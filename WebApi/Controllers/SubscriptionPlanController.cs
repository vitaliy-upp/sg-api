using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;


namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ILogger<SubscriptionPlanController> _logger;
        private readonly ISubscriptionPlanApplicationService _subscriptionPlanApplicationService;
        private readonly IUserApplicationService _userApplicationService;

        private readonly IMapper _mapper;

        public SubscriptionPlanController(ILogger<SubscriptionPlanController> logger
            , ISubscriptionPlanApplicationService subscriptionPlanApplicationService
            , IUserApplicationService userApplicationService
            , IMapper mapper)
        {
            _logger = logger;
            _subscriptionPlanApplicationService = subscriptionPlanApplicationService;
            _userApplicationService = userApplicationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Subscription Plans
        /// </summary>
        /// <returns>List of Subscription Plans</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetSubscriptionPlansDTO), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var plansDto = _subscriptionPlanApplicationService.GetSubscriptionPlans();
            return Ok(plansDto);
        }

        /// <summary>
        /// Setup subscription plan to company
        /// </summary>
        /// <returns>List of Subscription Plans</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post(UpdateCompanySubscriptionModel updateCompanySubscriptionModel)
        {
            try
            {
                var userModel = _userApplicationService.FindByIdentity(User.Identity);
                _subscriptionPlanApplicationService.UpdateCompanySubscription(updateCompanySubscriptionModel, userModel.Id);
            }catch(Exception ex)
            {
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
            return Ok();
        }

        /// <summary>
        /// Cancel subscription
        /// </summary>
        /// <returns>List of Subscription Plans</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete()
        {
            try
            {
                var userModel = _userApplicationService.FindByIdentity(User.Identity);
                _subscriptionPlanApplicationService.RemoveCompanySubscription(userModel.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
            return Ok();
        }
    }
}
