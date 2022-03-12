using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.Configurations
{
    public class SubscriptionFeatureConfiguration : IEntityTypeConfiguration<SubscriptionFeature>
    {
        public void Configure(EntityTypeBuilder<SubscriptionFeature> builder)
        {
            builder.HasData(
                new SubscriptionFeature { Id = 1, FeatureId = 1, NumberValue = 100, SubscriptionPlanId = 1 });
        }
    }
}
