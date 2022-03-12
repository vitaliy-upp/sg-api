using DataAccess.UserManagement;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using System.Linq;

namespace Domain.DataAccess.Services
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
