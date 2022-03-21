using System.Collections.Generic;
using System.Linq;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using FileManagement.DataAccess;

namespace Domain.DataAccess.Services
{
    public class SocialLinkRepository : DomainRepository<Attachment, int>, ISocialLinkRepository
    {
        public SocialLinkRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public void CreateAll(IEnumerable<Attachment> socialLinks)
        {
            Context.Set<Attachment>()
                .AddRange(socialLinks);
            SaveChanges();
        }

        public IList<Attachment> GetForUser(int userId)
        {
            return Context.Set<Attachment>().ToList();
        }
    }
}
