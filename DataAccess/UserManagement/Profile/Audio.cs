using Common.DataAccess;

namespace DataAccess.UserManagement
{
    public class Audio: Attachment, IBaseDomainModel<int>
    {
        public string Url { get; set; }
    }

}
