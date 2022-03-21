using Domain.DataAccess;
using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Entities.KidProfile.Education;
using Domain.Repository.Interfaces.KidProfile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Repository.KidProfile
{
    public class KidEducationRepository : DomainRepository<Education, int>, IKidEducationRepository
    {
        public KidEducationRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Education>> GetByKidId(int kidId)
        {
            return await Context.Set<Education>()
                .Where(t => t.KidProfileId == kidId)
                .ToListAsync();
        }
    }
}
