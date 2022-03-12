using Common.DataAccess;

namespace DataAccess.UserManagement
{
    public class Video : Attachment, IBaseDomainModel<int>
    {
        public string Url { get; set; }
    }

}
