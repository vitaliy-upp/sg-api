using System.ComponentModel.DataAnnotations;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class SuperPowerToKid {
        [Key]
        public int KidId { get; set; }
        
        [Key]
        public int SuperPowerId { get; set; }

        public KidProfile Kid { get; set; }
        public SuperPower SuperPower { get; set; }

    }
}
