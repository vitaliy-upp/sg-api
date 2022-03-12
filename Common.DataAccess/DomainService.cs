using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Common.DataAccess
{
    public class DomainService<TModel, TId> : IDomainService<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        protected readonly DbContext Context;

        protected DomainService(DbContext noLimitTechContext)
        {
            Context = noLimitTechContext;
        }

        protected DbSet<TModel> DbSet => Context.Set<TModel>();

        public virtual TModel GetById(TId id)
        {
            return Context.Set<TModel>().Find(id);
        }

        public virtual IList<TModel> GetByIds(IList<TId> ids)
        {
            return Context.Set<TModel>().Where(x => ids.Contains(x.Id)).ToList();
        }

        public virtual IList<TModel> GetAll()
        {
            return Context.Set<TModel>().ToList();
        }
        public virtual int GetCount()
        {
            return Context.Set<TModel>().Count();
        }
        public virtual TModel Create(TModel model)
        {
            Context.Set<TModel>().Add(model);
            SaveChanges();
            return model;
        }

        public virtual TModel Update(TModel updated)
        {
            if (updated == null)
            {
                return null;
            }

            var existing = GetById(updated.Id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                SaveChanges();
            }

            return existing;
        }

        public virtual TModel UpdateOrAdd(TModel model)
        {
            if (model == null)
            {
                return null;
            }

            var existing = GetById(model.Id);

            if (existing != null)
                Context.Entry(existing).CurrentValues.SetValues(model);
            else
                Context.Set<TModel>().Add(model);

            SaveChanges();


            return existing;
        }


        public virtual void Delete(TId id)
        {
            var entity = GetById(id);
            DbSet.Remove(entity);
            SaveChanges();
        }

        public virtual void Delete(IList<TId> ids)
        {
            foreach (var id in ids)
            {
                var entity = GetById(id);
                DbSet.Remove(entity);
            }

            SaveChanges();
        }

        public virtual void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
            SaveChanges();
        }

        public virtual void SaveChanges() => Context.SaveChanges();
    }
}
