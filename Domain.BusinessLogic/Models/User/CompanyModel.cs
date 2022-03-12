using System;
namespace Domain.BusinessLogic.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }

        public SubscriptionPlanDTO SubscriptionPlan { get; set; }
    }
}
