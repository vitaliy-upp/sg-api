using Common.DataAccess.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.DataAccess.Enitities
{
    public class SubscriptionFeature: IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public decimal? NumberValue { get; set; }
        public bool? FlagValue { get; set; }

        [ForeignKey("Feature")]
        public int FeatureId { get; set; }
        public Feature Feature{ get; set; }

        [ForeignKey("SubscriptionPlan")]
        public int SubscriptionPlanId { get; set; }

        public SubscriptionPlan SubscriptionPlan { get; set; }


    }
}
