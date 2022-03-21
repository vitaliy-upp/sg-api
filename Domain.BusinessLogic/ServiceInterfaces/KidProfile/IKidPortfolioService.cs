using Domain.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IKidPortfolioService : IBaseBusinessService
    {
        Task<IList<KidPortfolioDto>> GetAsync(int kidId);
        Task CreateAsync(int kidId, int parentId, AttachmentDto dto);
        Task DeleteAsync(int kidId, int parentId, int id);

    }
}
