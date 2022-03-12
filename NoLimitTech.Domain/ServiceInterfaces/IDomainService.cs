﻿using Common.DataAccess;
using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoLimitTech.Domain.ServiceInterfaces
{
    public interface IDomainService<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        TModel GetById(TId id);
        IList<TModel> GetByIds(IList<TId> ids);
        IList<TModel> GetAll();
        int GetCount();
        TModel Create(TModel model);
        Task<TModel> CreateAsync(TModel model);
        TModel Update(TModel updated);
        TModel UpdateOrAdd(TModel updated);
        void Delete(TId id);
        void Delete(IList<TId> ids);
        void Delete(TModel model);
        void SaveChanges();
    }
}
