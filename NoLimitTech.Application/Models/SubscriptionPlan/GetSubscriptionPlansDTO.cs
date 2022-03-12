using System.Collections.Generic;

namespace NoLimitTech.Application.Models
{
    public class GetSubscriptionPlansDTO
    {
        public int? CurrentPlanId { get; set; }
        public IEnumerable<SubscriptionPlanDTO> Subscriptions { get; set; }
    }
}
