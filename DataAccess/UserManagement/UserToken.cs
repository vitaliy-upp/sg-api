
using Common.DataAccess;

namespace DataAccess.UserManagement
{
    public class UserToken : Token, IBaseDomainModel<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public UserTokenTypeEnum UserTokenType { get; set; }

        public UserToken() : base()
        {

        }
    }
}
