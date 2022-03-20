namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IAppUrlProvider
    {
        string EventPageUrl(int eventId);
        string InviteLink(string token);
        string EmailActivationLink(string token);
    }
}
