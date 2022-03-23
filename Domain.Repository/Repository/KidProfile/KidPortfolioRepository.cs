using Domain.DataAccess.Entities.KidProfile;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class KidPortfolioRepository : DomainRepository<KidPortfolioItem, int>, IKidPortfolioRepository
    {
        public KidPortfolioRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAsync(int kidId, int attachmentId, bool? shouldBeSaved = false)
        {
            var item = await Context.Set<KidPortfolioItem>()
                .FirstOrDefaultAsync(t => t.KidId == kidId && t.AttachmentId == attachmentId);
            Context.Set<KidPortfolioItem>().Remove(item);
            await SaveChangesAsync(shouldBeSaved);
        }

        public async Task<IList<KidPortfolioItem>> GetByKidId(int kidId)
        {
            return await Context.Set<KidPortfolioItem>()
                .Where(t => t.KidId == kidId).ToListAsync();
        }

        public async Task CreateAsync(KidPortfolioItem item, bool? shouldBeSaved)
        {
            throw new System.NotImplementedException();
        }
    }

}
