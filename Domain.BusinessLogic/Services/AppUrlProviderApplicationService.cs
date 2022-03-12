using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessLogic.Services
{
    public class AppUrlProviderApplicationService : IAppUrlProviderApplicationService
    {
        private readonly ClientSideSettings _clientSideSettings;

        public AppUrlProviderApplicationService(IConfiguration configuration)
        {
            _clientSideSettings = configuration.GetSection(nameof(ClientSideSettings)).Get<ClientSideSettings>();

        }


        public string EventPageUrl(int eventId)
        {
            return $"{_clientSideSettings.Url}/event/{eventId}";
        }

        public string InviteLink(string token)
        {
            return $"{_clientSideSettings.Url}/invite/{token}";
        }

        public string EmailActivationLink(string token)
        {
            return $"{_clientSideSettings.Url}/auth/activate/{token}";
        }
    }
}
