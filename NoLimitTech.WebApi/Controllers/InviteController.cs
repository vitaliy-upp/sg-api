using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NoLimitTech.Application.Models;
using NoLimitTech.Application.Settings;
using NoLimitTech.Application.ServiceInterfaces;
using NoLimitTech.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        private readonly IInviteApplicationService _inviteApplicationService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly ILogger<InviteController> _logger;

        private readonly JwtSettings _jwtSettings;

        public InviteController(IInviteApplicationService inviteApplicationService
            , IUserApplicationService userApplicationService
            , IConfiguration configuration
            , ILogger<InviteController> logger)
        {
            _inviteApplicationService = inviteApplicationService;
            _userApplicationService = userApplicationService;
            _logger = logger;

            _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        }


        /// <summary>
        /// Getting invite model by invitation token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InviteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
        public IActionResult Get(string id)
        {
            // checking if an invite exists
            var invite = _inviteApplicationService.FindByToken(id);
            if (invite == null)
            { return BadRequest(new ApiErrorResponse("Invite is not valid")); }

            return Ok(invite);
        }


        #region OWNER/SUPERADMIN METHODS

        /// <summary>
        /// Edit invites (Owner or SuperAdmin only)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] CreateInviteModel model)
        {
            try
            {
                _inviteApplicationService.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
            return Ok();
        }

        /// <summary>
        /// Edit invites to event for each user from csv file (Owner or SuperAdmin only)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Bulk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Bulk([FromForm] BulkUploadInviteModel model)
        {
            try
            {
                _inviteApplicationService.BulkUpload(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create invite error.");
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
            return Ok();
        }

        /// <summary>
        /// Delete invitation
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            try
            {
                _inviteApplicationService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }

            return Ok();
        }

        #endregion
    }
}
