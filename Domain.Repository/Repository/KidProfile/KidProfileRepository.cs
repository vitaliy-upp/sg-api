using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class KidProfileRepository : DomainRepository<KidProfile, int>, IKidProfileRepository
    {
        public KidProfileRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<KidProfile>> GetByParentId(int parentId)
        {
            return await Context.Set<KidProfile>()
                //.Include(k => k.City)
                .Include(k => k.SuperPowers)
                .Where(t => t.ParrentId == parentId).ToListAsync();
        }
    }
}
