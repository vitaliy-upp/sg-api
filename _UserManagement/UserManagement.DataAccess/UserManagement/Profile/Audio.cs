using Common.DataAccess.Utilities;

namespace DataAccess.UserManagement
{
    public class Audio: Attachment, IBaseDomainModel<int>
    {
        public string Url { get; set; }
    }

}
