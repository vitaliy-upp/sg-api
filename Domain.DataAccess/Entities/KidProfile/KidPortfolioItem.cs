using Common.DataAccess.Utilities;
using FileManagement.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class KidPortfolioItem : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public int KidId { get; set; }
        public KidProfile KidProfile { get; set; }
    }
}
