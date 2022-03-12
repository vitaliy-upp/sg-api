
using System.Collections.Generic;

namespace MailSender.TemplateDataObjects
{
    public class InvitationMailData
    {
        public string EventName { get; set; }
        public List<string> EventDates { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string EventTimeZone { get; set; }
        public string EventDescription { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public string InvitationLink { get; set; }
        public string EventPageLink { get; set; }
        public string CompanyName { get; set; }

        public InvitationMailData()
        {
            EventDates = new List<string>();
        }
    }
}
