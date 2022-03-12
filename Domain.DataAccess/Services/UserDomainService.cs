using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using System.Linq;

namespace Domain.DataAccess.Services
{
    public class UserDomainService : DomainService<User, int>, IUserDomainService
    {
        public UserDomainService(DomainDbContext dbContext)
            : base(dbContext)
        {
        }

        public override User GetById(int id)
        {
            return Context.Set<User>()
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .SingleOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return Context.Set<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public User GetDetailedByEmail(string email)
        {
            return Context.Set<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Company)
                    .ThenInclude(c => c.SubscriptionPlan)
                        .ThenInclude(s => s.SubscriptionFeatures)
                            .ThenInclude(f => f.Feature)
                .SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public void SetUserRole(int userId, int roleId)
        {
            var ur = new UserRoles()
            {
                UserId = userId,
                RoleId = roleId
            };
            Context.Set<UserRoles>().Add(ur);

            SaveChanges();
        }

        public int GetUserRoleId(int userId)
        {
            return Context.Set<UserRoles>()
                .FirstOrDefault(x => x.UserId == userId).RoleId;
        }

        public User GetDetailedById(int userId)
        {
            return Context.Set<User>()
                .Include(u => u.UserRoles)
                .Include(u => u.Company)
                    .ThenInclude(c => c.SubscriptionPlan)
                        .ThenInclude(s => s.SubscriptionFeatures)
                            .ThenInclude(f => f.Feature)
                .FirstOrDefault(u => u.Id == userId);
        }

        public override User Update(User updated)
        {
            if (updated == null)
            {
                return null;
            }

            var existing = GetById(updated.Id);
            if (existing != null)
            {
                // to avoid unwanted changes
                updated.Email = existing.Email;
                updated.Password = existing.Password;

                Context.Entry(existing).CurrentValues.SetValues(updated);
                SaveChanges();
            }

            return existing;
        }

        public bool IsRegistered(string email)
        {
            return Context.Set<User>()
                .Any(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
