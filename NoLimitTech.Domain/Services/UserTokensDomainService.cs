using DataAccess.UserManagement;
using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;
using System.Linq;

namespace NoLimitTech.Domain.Services
{
    public class UserTokensDomainService : DomainService<UserToken, int>, IUserTokensDomainService
    {
        public UserTokensDomainService(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public UserToken Find(string token)
        {
            return Context.Set<UserToken>()
                .SingleOrDefault(x => x.TokenKey == token);
        }
    }
}
