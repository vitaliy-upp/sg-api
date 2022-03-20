using Common.DataAccess.Utilities;
using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IDomainRepository<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        Task<TModel> GetByIdAsync(TId id);
        IList<TModel> GetByIds(IList<TId> ids);
        int GetCount();
        TModel Create(TModel model);
        Task<TModel> CreateAsync(TModel model, bool? shouldSaveChanges = false);
        Task<TModel> UpdateAsync(TModel updated);
        //TModel UpdateOrAdd(TModel updated);
        Task DeleteAsync(TId id);
        Task DeleteAsync(IList<TId> ids);
        Task DeleteAsync(TModel model);
        Task SaveChangesAsync();

        Task<IList<TModel>> GetAllAsync();
    }
}
