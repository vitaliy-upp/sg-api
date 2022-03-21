using Domain.DataAccess.Entities.KidProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.Configurations
{
    public class PersonalityQuestionConfiguration : IEntityTypeConfiguration<PersonalityQuestion>
    {
        public void Configure(EntityTypeBuilder<PersonalityQuestion> builder)
        {
            builder.HasData(
                new PersonalityQuestion { Id = 1, Text = "What is your dream vacation?"},
                new PersonalityQuestion { Id = 2, Text = "If you could have a super power, what would it be?" },
                new PersonalityQuestion { Id = 2, Text = "What are you dreaming about?" }
                );
        }
    }
}
