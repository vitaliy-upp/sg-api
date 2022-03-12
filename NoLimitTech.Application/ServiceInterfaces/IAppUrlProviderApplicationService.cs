﻿namespace NoLimitTech.Application.ServiceInterfaces
{
    public interface IAppUrlProviderApplicationService
    {
        string EventPageUrl(int eventId);
        string InviteLink(string token);
        string EmailActivationLink(string token);
    }
}