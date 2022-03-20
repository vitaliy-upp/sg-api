using Common.DataAccess.Utilities;
using DataAccess;
using Domain.DataAccess.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace Domain.DataAccess.Models
{
    public class MessageAttachment : IBaseDomainModel<int>, ICreatedDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; private set; }
        public MessageAttachmentTypeEnum Type { get; set; }

        /// <summary>
        /// Url to CDN storage
        /// </summary>
        public string FilePath { get; set; }
        public string FileExtension { get; set; }

        public MessageAttachment() {
            CreatedDate = DateTimeUtils.Now();
        }
    }
}
