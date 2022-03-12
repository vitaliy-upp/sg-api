using DataAccess.UserManagement;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;

namespace Domain.DataAccess.Services
{
    public class RoleDomainService : DomainService<Role, int>, IRoleDomainService
    {
        public RoleDomainService(DomainDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
