using System;
using System.ComponentModel.DataAnnotations;
using Common.DataAccess.Utilities;

namespace FileManagement.DataAccess
{
    public class Attachment : IBaseDomainModel<int>, ICreatedDate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public AttachmentTypeEnum Type { get; set; }
        public string MimeType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
    }
}
