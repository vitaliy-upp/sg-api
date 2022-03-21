using System;

namespace Domain.BusinessLogic.Models
{
    public class EducationItemDto
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public bool IsUpToPresent { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Grade { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }


    }
}
