using Common.DataAccess;
using DataAccess;
using NoLimitTech.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace NoLimitTech.Domain.Models
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
