using Domain.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IKidEducationService : IBaseBusinessService
    {
        Task<EducationProfileDto> GetByKidIdAsync(int kidId);
        Task UpdateAsync(int kidId, int parentId, EducationProfileDto model);
        Task CreateAsync(int kidId, int parentId, EducationProfileDto model);
    }
}
