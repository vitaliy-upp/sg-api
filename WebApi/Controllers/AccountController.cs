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

namespace NoLimitTech.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;
        private readonly IInviteApplicationService _inviteApplicationService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserTokensApplicationService _userTokensApplicationService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAppUrlProviderApplicationService _appUrlProviderApplicationService;

        private readonly JwtSettings _jwtSettings;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly EmailProviderSettings _emailProviderSettings;

        public AccountController(IConfiguration configuration
            , IAppUrlProviderApplicationService appUrlProviderApplicationService
            , IUserApplicationService userApplicationService
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
            _appUrlProviderApplicationService = appUrlProviderApplicationService;

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
        public IActionResult Token([FromBody] LoginModel model)
        {
            var userModel = AuthenticateUser(model);
            if (userModel == null)
            {
                return NotFound(new ApiErrorResponse("Invalid username or password."));
            }

            string jwtToken = JwtTokenUtils.GenerateJwtToken(userModel.Email, userModel.Role, true, false, _jwtSettings);

            return Ok(new TokenResponseModel { Token = jwtToken, UserDetails = userModel });
        }

        /// <summary>
        /// Getting an access token for unregistered users by event token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("uToken")]
        [ProducesResponseType(typeof(uTokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnregisteredToken([FromForm] ULoginModel model)
        {
            try
            {

                //bool isRegistered = _userApplicationService.IsRegistered();
                //string jwtToken = JwtTokenUtils.GenerateJwtToken(eventUser.Invite.UserEmail, UserRolesEnum.User, isRegistered, true, _jwtSettings);

                //var eventUserModel = _mapper.Map<EventUserModel>(eventUser);

                return Ok(new uTokenResponseModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ApiErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Registration of user
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
            if (_userApplicationService.IfExists(model.Email))
            { return BadRequest(new ApiErrorResponse("A user with this email address already exists")); }

            try
            {
                // Creating new user
                UserModel userModel = await _userApplicationService.CreateAsync(model);

                // generate email verification token key
                UserTokenModel tokenModel = _userTokensApplicationService.CreateEmailVerificationToken(userModel.Id);

                SignUpConfirmationMailData mailData = new SignUpConfirmationMailData()
                {
                    UserEmail = userModel.Email,
                    UserName = userModel.FullName,
                    ConfirmationLink = _appUrlProviderApplicationService.EmailActivationLink(tokenModel.TokenKey)
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
            var invite = _inviteApplicationService.FindByToken(model.InviteKey);
            if (invite == null)
            { return NotFound(new ApiErrorResponse("Invite was not found")); }

            UserModel userModel;
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
        public IActionResult Activate([FromBody] ActivationModel model)
        {
            var tokenModel = _userTokensApplicationService.Find(model.Token);
            if (tokenModel == null)
            { return NotFound(new ApiErrorResponse("Token is not valid")); }

            // TODO: maybe should set expired date of the token to today
            var userModel = _userApplicationService.FindById(tokenModel.UserId);
            if (userModel == null)
            { return NotFound(new ApiErrorResponse("User was not found")); }

            try
            {
                // confirming an email
                _userApplicationService.ConfirmEmail(userModel.Id);
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
        public IActionResult ValidateEmail([FromBody] EmailDTO model)
        {
            bool exists = _userApplicationService.IfExists(model.Email);
            return Ok(new { status = exists });
        }

        #region PRIVATE METHODS

        private UserModel AuthenticateUser(LoginModel model)
        {
            if (model == null) return null;

            var person = _userApplicationService.FindByLoginPassword(model.Username, model.Password);
            return person;
        }

        #endregion
    }
}
