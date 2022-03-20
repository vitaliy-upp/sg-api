using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Entities.KidProfile;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface ISuperPowerService : IBusinessService<SuperPower, SuperPowerDto>
    {
      
    }
}
