using Common.DataAccess.Utilities;
using DataAccess;
using Domain.DataAccess.Enums;
using System;
using System.Collections.Generic;
using Utilities;

namespace Domain.DataAccess.Models
{
    public class Message : IBaseDomainModel<int>, ICreatedDate
    {
        public int Id { get; set; }
        public int? ToDeskId { get; set; }
        public int? ToUserId { get; set; }
        public int? ToFloorId { get; set; }
        public int? ToConferenceId { get; set; }
        public MessageTypeEnum Type { get; set; }
        public int FromUserId { get; set; }
        public string Text { get; set; }
        public ICollection<MessageAttachment> Attachments { get; set; }
        public DateTime CreatedDate { get; private set; }

        public Message()
        {
            CreatedDate = DateTimeUtils.Now();
            Type = MessageTypeEnum.Regular;
        }
    }
}
