using Domain.DataAccess.Entities.KidProfile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public interface IKidPortfolioRepository
    {
        Task<IList<KidPortfolioItem>> GetByKidId(int kidId);
        Task DeleteAsync(int kidId, int attachmentId, bool? shouldBeSaved = false);
        Task CreateAsync(KidPortfolioItem item, bool? shouldBeSaved = false);

    }
}