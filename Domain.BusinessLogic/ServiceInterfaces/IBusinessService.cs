using Common.DataAccess.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IBusinessService<TModel, TDto>: IBaseBusinessService where TModel : class
    {
        Task<IList<TModel>> GetAllAsync();
    }
}
