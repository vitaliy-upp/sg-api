using MailSender.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ISendGridClient _sendGridClient;

        public EmailSenderService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendTemplateEmailAsync(string emailFrom, string nameFrom, string emailTo, string nameTo, string templateId, object dynamicData)
        {
            EmailAddress from = new EmailAddress(emailFrom, nameFrom);
            EmailAddress to = new EmailAddress(emailTo, nameTo);
            SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicData);
            var response = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
