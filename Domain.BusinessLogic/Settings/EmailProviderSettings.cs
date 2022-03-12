namespace Domain.BusinessLogic.Settings
{
    public class EmailProviderSettings
    {
        public string From { get; set; }
        public string NameFrom { get; set; }
        public string ApiKey { get; set; }
        public bool SandboxMode { get; set; }

        public string InvitationTId { get; set; }
        public string SignUpConfirmationTId { get; set; }
        public string ResetPasswordTId { get; set; }
    }
}
