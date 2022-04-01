using DataAccess.UserManagement;
using Domain.DataAccess.Entities.KidProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.DataAccess.Configurations
{
    public class SuperPowerConfiguration : IEntityTypeConfiguration<SuperPower>
    {
        public void Configure(EntityTypeBuilder<SuperPower> builder)
        {
            //builder.HasData(
            //    new SuperPower { Id = 1, Name = "Music" },
            //    new SuperPower { Id = 2, Name = "Musician (playing an instrument). Piano", ParentId = 1 },
            //    new SuperPower { Id = 3, Name = "Musician (playing an instrument). Guitar", ParentId = 1 },
            //    new SuperPower { Id = 4, Name = "Musician (playing an instrument). Drums", ParentId = 1 },
            //    new SuperPower { Id = 5, Name = "Singer. Yodeler", ParentId = 1 },
            //    new SuperPower { Id = 6, Name = "Singer. Country singer", ParentId = 1 },
            //    new SuperPower { Id = 7, Name = "Singer. Band", ParentId = 1 },
            //    new SuperPower { Id = 8, Name = "Singer. Country", ParentId = 1 },
            //    new SuperPower { Id = 9, Name = "Singer. Opera singer", ParentId = 1 },
            //    new SuperPower { Id = 10, Name = "Singer. Lip sync", ParentId = 1 },
            //    new SuperPower { Id = 11, Name = "Choir", ParentId = 1 },
            //    new SuperPower { Id = 12, Name = "Jazz Band", ParentId = 1 },
            //    new SuperPower { Id = 13, Name = "Marching band", ParentId = 1 },
            //    new SuperPower { Id = 14, Name = "Orchestra", ParentId = 1 },
            //    new SuperPower { Id = 15, Name = "Percussion", ParentId = 1 },
            //    new SuperPower { Id = 16, Name = "Song writer", ParentId = 1 },
            //    new SuperPower { Id = 17, Name = "DJ", ParentId = 1 },
            //    new SuperPower { Id = 18, Name = "Drummer", ParentId = 1 },

            //    new SuperPower { Id = 19, Name = "Dance"},
            //    new SuperPower { Id = 20, Name = "Solo dancer", ParentId = 19 },
            //    new SuperPower { Id = 21, Name = "Dance group", ParentId = 19 },
            //    new SuperPower { Id = 22, Name = "Dance troupe", ParentId = 19 },
            //    new SuperPower { Id = 23, Name = "Hip Hop", ParentId = 19 },
            //    new SuperPower { Id = 24, Name = "Ballet", ParentId = 19 },
            //    new SuperPower { Id = 25, Name = "Ballroom", ParentId = 19 },
            //    new SuperPower { Id = 26, Name = "Contemporary", ParentId = 19 },
            //    new SuperPower { Id = 27, Name = "Jazz", ParentId = 19 },
            //    new SuperPower { Id = 28, Name = "Tap Dance", ParentId = 19 },
            //    new SuperPower { Id = 29, Name = "Folk Dance", ParentId = 19 },
            //    new SuperPower { Id = 30, Name = "Irish Dance", ParentId = 19 },
            //    new SuperPower { Id = 31, Name = "Modern Dance", ParentId = 19 },
            //    new SuperPower { Id = 32, Name = "Swing Dance", ParentId = 19 },
            //    new SuperPower { Id = 33, Name = "Fashion" }
            //    );
        }
    }
}
