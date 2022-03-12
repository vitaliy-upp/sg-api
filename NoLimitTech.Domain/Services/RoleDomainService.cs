using DataAccess.UserManagement;
using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;

namespace NoLimitTech.Domain.Services
{
    public class RoleDomainService : DomainService<Role, int>, IRoleDomainService
    {
        public RoleDomainService(DomainDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
