using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.DataAccess.Utilities
{
    public interface IDomainService<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        Task<TModel> GetByIdAsync(TId id);
        IList<TModel> GetByIds(IList<TId> ids);
        int GetCount();
        TModel Create(TModel model);
        Task<TModel> UpdateAsync(TModel updated);
        //TModel UpdateOrAdd(TModel updated);
        Task DeleteAsync(TId id);
        Task DeleteAsync(IList<TId> ids);
        Task DeleteAsync(TModel model);
        void SaveChanges();
    }
}
