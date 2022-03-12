using Common.DataAccess.Utilities;
using Payment.DataAccess.Enum;
using System.Collections.Generic;

namespace Payment.DataAccess.Enitities
{
    public class SubscriptionPlan: IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Annual { get; set; }
        public SubscriptionPlanTypeEnum Type { get; set; }

        public ICollection<SubscriptionFeature> SubscriptionFeatures { get; set; }

        public FreeTrialPeriodEnum FreeTrialPeriod { get; set; }
    }
}
