using Common.DataAccess.Utilities;
using DataAccess;

namespace DataAccess.UserManagement
{
    public class ExternalLink : Attachment, IBaseDomainModel<int>
    {
        public string Url { get; set; }
    }

}
