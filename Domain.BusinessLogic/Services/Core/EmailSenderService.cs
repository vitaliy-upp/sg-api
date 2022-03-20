//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Domain.BusinessLogic.ServiceInterfaces;
//using Domain.BusinessLogic.Settings;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System.Threading.Tasks;

//namespace Domain.BusinessLogic.Services
//{
//    public class EmailSenderService : IEmailSenderService
//    {
//        private readonly ISendGridClient _sendGridClient;
//        private readonly ILogger<EmailSenderService> _logger;

//        private readonly EmailProviderSettings _providerSettings;

//        public EmailSenderService(ISendGridClient sendGridClient
//            , IConfiguration configuration
//            , ILogger<EmailSenderService> logger)
//        {
//            _sendGridClient = sendGridClient;
//            _logger = logger;

//            _providerSettings = configuration.GetSection(nameof(EmailProviderSettings)).Get<EmailProviderSettings>();
//        }

//        public async Task SendEmailAsync(string emailTo, string nameTo, string subject, string message)
//        {
//            var from = new EmailAddress(_providerSettings.From, "vSummit Team");
//            var to = new EmailAddress(emailTo, nameTo);
//            var htmlText = string.Format("<div>{0}</div>", message);
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlText);
//            var response = await _sendGridClient.SendEmailAsync(msg);
//        }

//        public async Task SendTemplateEmailAsync(string emailTo, string nameTo)
//        {
//            EmailAddress from = new EmailAddress(_providerSettings.From, "Vsummits Team");
//            EmailAddress to = new EmailAddress(emailTo, nameTo);
//            SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(from, to, _providerSettings.InvitationTId,)
//        }
//    }
//}
