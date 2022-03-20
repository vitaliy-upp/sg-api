using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using Domain.BusinessLogic.Extensions;

namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userApplicationService;
        private readonly ILogger<UserProfileController> _logger;


        public UserProfileController(IUserService userApplicationService
            , ILogger<UserProfileController> logger)
        {
            _userApplicationService = userApplicationService;
            _logger = logger;
        }


        /// <summary>
        /// Get User Profile data
        /// </summary>
        /// <returns>User Model</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var userModel = await _userApplicationService.GetUserByIdentityAsync(User.Identity);
            return Ok(userModel);
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="model">Update User Model</param>
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromForm] UpdateUserModel model)
        {
            var userEmail = (User.Identity as ClaimsIdentity).GetValueByClaimType(ClaimTypes.Email);
            try
            {
                var updatedUser = await _userApplicationService.UpdateAsync(userEmail, model);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
