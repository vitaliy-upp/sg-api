using System.Collections.Generic;
using System.Linq;
using DataAccess.UserManagement;
using NoLimitTech.Domain.ServiceInterfaces;

namespace NoLimitTech.Domain.Services
{
    public class SocialLinkRepository : DomainService<ExternalLink, int>, ISocialLinkRepository
    {
        public SocialLinkRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public void CreateAll(IEnumerable<ExternalLink> socialLinks)
        {
            Context.Set<ExternalLink>()
                .AddRange(socialLinks);
            SaveChanges();
        }

        public IList<ExternalLink> GetForUser(int userId)
        {
            return Context.Set<ExternalLink>().ToList();
        }
    }
}
