using DataAccess.UserManagement;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class UserTokensDomainRepository : DomainRepository<UserToken, int>, IUserTokensDomainService
    {
        public UserTokensDomainRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<UserToken> FindAsync(string token)
        {
            return await Context.Set<UserToken>()
                .FirstOrDefaultAsync(x => x.TokenKey == token);
        }
    }
}
