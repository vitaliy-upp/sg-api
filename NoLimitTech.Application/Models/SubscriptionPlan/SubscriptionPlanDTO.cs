using System.Collections.Generic;
using Payment.DataAccess.Enum;

namespace NoLimitTech.Application.Models
{
    public class SubscriptionPlanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Annual { get; set; }
        public SubscriptionPlanTypeEnum Type { get; set; }

        public IList<SubscriptionFeatureDTO> Features { get; set; }

        public FreeTrialPeriodEnum FreeTrialPeriod { get; set; }


        public string PriceUnits => AppConstants.PRICE_UNITS;
        public string TrialPeriodUnits => AppConstants.TRIAL_PERIOD_UNITS;
        public string DurationUnits => AppConstants.DURATION_UNITS;
        public string HDRecordingUnits => AppConstants.HD_RECORDING_UNITS;
    }
}
