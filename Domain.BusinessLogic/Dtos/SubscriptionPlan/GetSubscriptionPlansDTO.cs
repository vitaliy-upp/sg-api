using System.Collections.Generic;

namespace Domain.BusinessLogic.Models
{
    public class GetSubscriptionPlansDTO
    {
        public int? CurrentPlanId { get; set; }
        public IEnumerable<SubscriptionPlanDTO> Subscriptions { get; set; }
    }
}
