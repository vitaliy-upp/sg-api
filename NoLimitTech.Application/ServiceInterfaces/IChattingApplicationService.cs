﻿using NoLimitTech.Application.Models;

namespace NoLimitTech.Application.ServiceInterfaces
{
    public interface IChattingApplicationService
    {
        /// <summary>
        /// Save message
        /// </summary>
        /// <param name="fromUserId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ChatMessageModel SaveMessage(int fromUserId, int eventId, ESSendMessageModel model);
    }
}
