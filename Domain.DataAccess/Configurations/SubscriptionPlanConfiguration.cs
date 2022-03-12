using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.DataAccess.Enitities;
using Payment.DataAccess.Enum;

namespace Domain.DataAccess.Configurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasData(
                //new SubscriptionPlan { Id = 1, Name = "Presenter", Price = 15, Annual = 180, FreeTrialPeriod = FreeTrialPeriodEnum.Month, Type = SubscriptionPlanTypeEnum.Recurring },
                new SubscriptionPlan { Id = 1, Name = "Organizer", Price = 99, FreeTrialPeriod = FreeTrialPeriodEnum.Month, Type = SubscriptionPlanTypeEnum.Recurring }
                );
        }
    }
}
