using System;
using System.Threading.Tasks;
using MailSender.Interfaces;
using MailSender.TemplateDataObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Models.User;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Settings;

namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IUserService _userApplicationService;
        private readonly IUserTokensApplicationService _userTokensApplicationService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<PasswordController> _logger;

        private readonly ClientSideSettings _clientSideSettings;
        private readonly EmailProviderSettings _emailProviderSettings;

        public PasswordController(IUserService userApplicationService
            , IUserTokensApplicationService userTokensApplicationService
            , IEmailSenderService emailSenderService
            , IConfiguration configuration
            , ILogger<PasswordController> logger)
        {
            _userApplicationService = userApplicationService;
            _userTokensApplicationService = userTokensApplicationService;
            _emailSenderService = emailSenderService;
            _logger = logger;

            _clientSideSettings = configuration.GetSection(nameof(ClientSideSettings)).Get<ClientSideSettings>();
            _emailProviderSettings = configuration.GetSection(nameof(EmailProviderSettings)).Get<EmailProviderSettings>();
        }


        
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] ChangePasswordModel model)
        {
            var userModel = await _userApplicationService.GetUserByIdentityAsync(User.Identity);
            try
            {
                await _userApplicationService.ChangePasswordAsync(userModel.Id, model.OldPassword, model.NewPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
            return Ok();
        }

        /// <summary>
        /// Request reset password (send an email with a reset link)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok | BadRequest</returns>
        [AllowAnonymous]
        [HttpPost("reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RequesResetPassword(RequestResetPasswordModel model)
        {
            UserDto user = await _userApplicationService.GetByEmailAsync(model.Email);
            if(user == null)
            { return NotFound(new ApiErrorResponse("User with specified email was not found.")); }

            try
            {
                UserTokenDto userTokenModel = _userTokensApplicationService.CreateResetPasswordToken(user.Id, 1);

                ResetPasswordMailData mailData = new ResetPasswordMailData()
                {
                    UserName = user.FullName,
                    ResetPasswordLink = $"{_clientSideSettings.Url}/auth/request-reset-password/{userTokenModel.TokenKey}",
                    LetUsKnowLink = ""  // TODO: add link
                };

                await _emailSenderService.SendTemplateEmailAsync(_emailProviderSettings.From, _emailProviderSettings.NameFrom,
                    user.Email, user.FullName, _emailProviderSettings.ResetPasswordTId, mailData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok | BadRequest</returns>
        [AllowAnonymous]
        [HttpPut("reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            UserTokenDto tokenModel = await _userTokensApplicationService.FindAsync(model.ResetToken);
            if (tokenModel == null || tokenModel.ExpiredDate < DateTime.Now)
            { return BadRequest(new ApiErrorResponse("Token is not valid")); }

            try
            {
                await _userApplicationService.ChangePasswordAsync(tokenModel.UserId, model.NewPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }            

            return Ok();
        }
    }
}
