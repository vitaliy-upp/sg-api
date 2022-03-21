using Domain.DataAccess.Entities.KidProfile.Education;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository.Interfaces.KidProfile
{
    public interface IKidEducationRepository : IDomainRepository<Education, int>
    {
        Task<IList<Education>> GetByKidId(int kidId);

    }
}
