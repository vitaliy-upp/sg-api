using DataAccess.UserManagement;
using Domain.DataAccess.Entities.KidProfile;
using Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IKidProfileRepository : IDomainRepository<KidProfile, int>
    {
        Task<IList<KidProfile>> GetByParentId(int parentId);
    }
}
