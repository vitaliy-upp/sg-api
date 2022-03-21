using System.Collections.Generic;

namespace Domain.BusinessLogic.Models
{
    public class KidPortfolioDto
    {
        public int Id { get; set; }

        public IList<AttachmentDto> Photos { get; set; }
        public IList<AttachmentDto> Videos { get; set; }

        public IList<AttachmentDto> Audio { get; set; }

        public IList<AttachmentDto> Docs { get; set; }
        public IList<AttachmentDto> Links { get; set; }


    }
}