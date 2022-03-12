using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic;
using Domain.BusinessLogic.Extensions;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.Enums;
using System;
using System.Threading.Tasks;

namespace NoLimitTech.WebApi.SignalR
{
    [Authorize]
    public class CustomHub : Hub
    {
        private readonly ILogger<CustomHub> _logger;

        #region WEBSOCKET MESSAGES

        public const string UserAddedToChat = "UserAddedToChat";
        public const string UserComeIn = "UserComeIn";
        public const string UserСonnected = "UserСonnected";
        public const string UserDeskData = "UserDeskData";
        public const string UserDisconnected = "UserDisconnected";
        public const string UserEndPresent = "UserEndPresent";
        public const string UserFloorData = "UserFloorData";
        public const string UserFloors = "UserFloors";
        public const string UserGetUp = "UserGetUp";
        public const string UserGoOut = "UserGoOut";
        public const string UserLeaveChatRoom = "UserLeaveChatRoom";
        public const string UserReceiveMessage = "UserReceiveMessage";
        public const string UserStartPresent = "UserStartPresent";
        public const string UserTakeASeat = "UserTakeASeat";
        public const string UserSetSettings = "UserSetSettings";
        public const string EventSpaceBlock = "EventSpaceBlock";
        public const string EventSpaceUnblock = "EventSpaceUnblock";
        public const string EventSpaceJoin = "EventSpaceJoin";
        public const string EventSpaceKick = "EventSpaceKick";
        public const string EventSpaceMute = "EventSpaceMute";
        public const string EventSpaceOverview = "EventSpaceOverview";
        public const string EventSpaceNoise = "EventSpaceNoise";


        #endregion

        public CustomHub(ILogger<CustomHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                int eventId = Convert.ToInt32(Context.GetHttpContext().Request.Query["eventId"]);

                // adding user to a event group
                await this.Groups.AddToGroupAsync(Context.ConnectionId, AppConstants.WEBSOCKET_EVENT_GROUP_ + eventId);

                await this.Clients.Caller.SendAsync(CustomHub.EventSpaceOverview);

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }

}
