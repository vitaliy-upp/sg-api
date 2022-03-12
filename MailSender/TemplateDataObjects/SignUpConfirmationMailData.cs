namespace MailSender.TemplateDataObjects
{
    public class SignUpConfirmationMailData
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ConfirmationLink { get; set; }
    }
}
