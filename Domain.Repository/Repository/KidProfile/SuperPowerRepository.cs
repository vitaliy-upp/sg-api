using Domain.DataAccess;
using Domain.DataAccess.Entities.KidProfile;
using Domain.Repository.Interfaces.KidProfile;

namespace Domain.Repository.Repository.KidProfile
{
    public class SuperPowerRepository : DomainRepository<SuperPower, int>, ISuperPowerRepository
    {
        public SuperPowerRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

    }
}
