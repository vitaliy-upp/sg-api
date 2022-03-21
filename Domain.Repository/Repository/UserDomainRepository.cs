using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using System.Linq;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class UserDomainRepository : DomainRepository<User, int>, IUserRepository
    {
        public UserDomainRepository(DomainDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await Context.Set<User>()
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.Set<User>()
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetDetailedByEmailAsync(string email)
        {
            return await Context.Set<User>()
                .Include(u => u.UserRoles)
                .Include(u => u.Company)
                    .ThenInclude(c => c.SubscriptionPlan)
                        .ThenInclude(s => s.SubscriptionFeatures)
                            .ThenInclude(f => f.Feature)
                .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task SetUserRoleAsync(int userId, int roleId)
        {
            var ur = new UserRoles()
            {
                UserId = userId,
                RoleId = roleId
            };
            await Context.Set<UserRoles>().AddAsync(ur);

            await SaveChangesAsync();
        }

        public async Task<User> GetDetailedByIdAsync(int userId)
        {
            return await Context.Set<User>()
                .Include(u => u.UserRoles)
                .Include(u => u.Company)
                    .ThenInclude(c => c.SubscriptionPlan)
                        .ThenInclude(s => s.SubscriptionFeatures)
                            .ThenInclude(f => f.Feature)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public override async Task<User> UpdateAsync(User updated, bool? shouldBeUpdated = false)
        {
            if (updated == null)
            {
                return null;
            }

            //TODO: Refactor it and optimize
            var existing = await GetByIdAsync(updated.Id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                if (shouldBeUpdated.HasValue && shouldBeUpdated.Value)
                {
                    await SaveChangesAsync();
                }
            }

            return existing;
        }

        public async Task<bool> CheckEmailAvailabilityAsync(string email)
        {
            var isOccupied = await Context.Set<User>()
                .AnyAsync(x => x.Email.ToLower() == email.ToLower());
            return !isOccupied;
        }
    }
}
