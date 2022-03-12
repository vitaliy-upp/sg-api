using Domain.DataAccess.Enums;
using System;

namespace Domain.BusinessLogic.Models
{
    public class ChatMessageModel
    {
        public int? ToDeskId { get; set; }
        public int? ToUserId { get; set; }
        public int? ToFloorId { get; set; }
        public int? ToConferenceId { get; set; }
        public MessageTypeEnum Type { get; set; }
        public UserMessageDTO FromUser { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; private set; }
    }
}
