using Common.DataAccess.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class KidPortfolio : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public int KidId { get; set; }
        public KidProfile KidProfile { get; set; }
    }
}
