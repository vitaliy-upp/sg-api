using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSender.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Send a template email
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="nameFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="nameTo"></param>
        /// <param name="templateId"></param>
        /// <param name="dynamicData"></param>
        /// <returns></returns>
        Task SendTemplateEmailAsync(string emailFrom, string nameFrom, string emailTo, string nameTo, string templateId, object dynamicData);
    }
}
