using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class ESSendMessageModel
    {
        [Required]
        public string Text { get; set; }

        /// <summary>[DEPRECATED] will be removed in future</summary>
        //[Required]
        public int ToConferenceId { get; set; }
        public int? ToDeskId { get; set; }
        public int? ToUserId { get; set; }
        /// <summary>[DEPRECATED] will be removed in future</summary>
        public int? ToFloorId { get; set; }



        public string GetChatGroupName(int eventId)
        {
            if (ToDeskId.HasValue)
                return AppConstants.WEBSOCKET_DESK_GROUP_ + ToDeskId.Value;
            //else if (ToFloorId.HasValue)
            //    return AppConstants.WEBSOCKET_FLOOR_GROUP_ + ToFloorId.Value;
            else
                return AppConstants.WEBSOCKET_EVENT_GROUP_ + eventId;
        }
    }
}
