using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Utils;
using Domain.BusinessLogic.Enums;
using AutoMapper;
using Domain.BusinessLogic.Models.User;
using MailSender.Interfaces;
using MailSender.TemplateDataObjects;
using Domain.DataAccess.Enums;
using System.Net;
using FileManagement.Utilities.AzureBlob;

namespace NoLimitTech.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userApplicationService;
        private readonly IInviteApplicationService _inviteApplicationService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserTokensApplicationService _userTokensApplicationService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAppUrlProvider _appUrlProvider;

        private readonly JwtSettings _jwtSettings;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly EmailProviderSettings _emailProviderSettings;

        public AccountController(IConfiguration configuration
            , IAppUrlProvider appUrlProviderApplicationService
            , IUserService userApplicationService
            , IInviteApplicationService inviteApplicationService
            , IEmailSenderService emailSenderService
            , IUserTokensApplicationService userTokensApplicationService
            , ILogger<AccountController> logger
            , IMapper mapper)
        {
            _userApplicationService = userApplicationService;
            _inviteApplicationService = inviteApplicationService;
            _userTokensApplicationService = userTokensApplicationService;
            _emailSenderService = emailSenderService;
            _logger = logger;
            _mapper = mapper;
            _appUrlProvider = appUrlProviderApplicationService;

            _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            _blobStorageSettings = configuration.GetSection(nameof(BlobStorageSettings)).Get<BlobStorageSettings>();
            //_clientSideSettings = configuration.GetSection(nameof(ClientSideSettings)).Get<ClientSideSettings>();
            _emailProviderSettings = configuration.GetSection(nameof(EmailProviderSettings)).Get<EmailProviderSettings>();
        }

        /// <summary>
        /// Getting an access token for registered users
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An object contained access token and user details</returns>
        /// <response code="200">If Ok</response>
        /// <response code="404">Invalid username or password</response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            if (model == null) return null;

            var userModel = await _userApplicationService.GetByLoginPasswordAsync(model.Username, model.Password);

            string jwtToken = JwtTokenUtils.GenerateJwtToken(userModel.Email, userModel.Role, true, false, _jwtSettings);

            return Ok(new TokenResponseModel { Token = jwtToken, UserDetails = userModel });
        }

        /// <summary>
        /// Registration of a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success</returns>
        /// <response code="200">If Ok</response>
        /// <response code="400">If model is wrong or user already exists</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromForm] RegistrationModel model)
        {
            var isAvailable = await _userApplicationService.CheckEmailAvailabilityAsync(model.Email);
            if (!isAvailable)
            {
                return BadRequest(new ApiErrorResponse("A user with this email address already exists"));
            }

            try
            {
                // Creating new user
                UserDto userModel = await _userApplicationService.CreateAsync(model);

                // generate email verification token key
                UserTokenDto tokenModel = await _userTokensApplicationService.CreateEmailVerificationTokenAsync(userModel.Id);
                var confirmationLink = _appUrlProvider.EmailActivationLink(tokenModel.TokenKey);

                SignUpConfirmationMailData mailData = new SignUpConfirmationMailData()
                {
                    UserEmail = userModel.Email,
                    UserName = userModel.FullName,
                    ConfirmationLink = confirmationLink
                };

                // sending email
                await _emailSenderService.SendTemplateEmailAsync(_emailProviderSettings.From, _emailProviderSettings.NameFrom,
                    userModel.Email, userModel.FullName, _emailProviderSettings.SignUpConfirmationTId, mailData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }

            return Ok();
        }

        /// <summary>
        /// Registration by invite key
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("uregister")]
        [ProducesResponseType(typeof(TokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterByToken([FromForm] URegistrationModel model)
        {
            var invite = await _inviteApplicationService.FindByTokenAsync(model.InviteKey);
            if (invite == null)
            {
                return NotFound(new ApiErrorResponse("Invite was not found"));
            }

            UserDto userModel;
            try
            {
                // Creating new user
                userModel = await _userApplicationService.CreateAsync(invite.UserEmail, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

            string jwtToken = JwtTokenUtils.GenerateJwtToken(invite.UserEmail, UserRolesEnum.User, true, true, _jwtSettings);

            return Ok(new TokenResponseModel { Token = jwtToken, UserDetails = userModel });
        }

        /// <summary>
        /// Activate user account by confirmation email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate([FromBody] ActivationModel model)
        {
            var tokenModel = await _userTokensApplicationService.FindAsync(model.Token);
            if (tokenModel == null)
            { 
                return NotFound(new ApiErrorResponse("Token is not valid"));
            }

            // TODO: maybe should set expired date of the token to today
            var userModel = await _userApplicationService.GetByIdAsync(tokenModel.UserId);
            if (userModel == null)
            { 
                return NotFound(new ApiErrorResponse("User was not found")); 
            }

            try
            {
                // confirming an email
                await _userApplicationService.ConfirmEmailAsync(userModel.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }

            return Ok();
        }

        /// <summary>
        /// Check if email address already exists
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("check/email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateEmail([FromBody] EmailDTO model)
        {
            bool isAvailabe = await _userApplicationService.CheckEmailAvailabilityAsync(model.Email);
            return Ok(new { status = !isAvailabe });
        }

    }
}
