using Common.DataAccess.Utilities;

namespace DataAccess.UserManagement
{
    public class Video : Attachment, IBaseDomainModel<int>
    {
        public string Url { get; set; }
    }

}
