namespace NoLimitTech.Application.Settings
{
    public class StripeOutcomeUrlSettings
    {
        public string TicketPaymentSuccess { get; set; }
        public string TicketPaymentCancel { get; set; }
        public string SubscriptionPaymentSuccess { get; set; }
        public string SubscriptionPaymentCancel { get; set; }


        public string GetTicketPaymentCancelUrl(int eventId)
        {
            return string.Format(TicketPaymentCancel, eventId);
        }
    }
}
