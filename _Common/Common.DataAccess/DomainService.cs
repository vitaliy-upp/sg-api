using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.Utilities
{
    public class DomainService<TModel, TId> : IDomainService<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        protected readonly DbContext Context;

        protected DomainService(DbContext noLimitTechContext)
        {
            Context = noLimitTechContext;
        }

        protected DbSet<TModel> DbSet => Context.Set<TModel>();

        public virtual async Task<TModel> GetByIdAsync(TId id)
        {
            return await Context.Set<TModel>().FindAsync(id);
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

        public virtual async Task<TModel> UpdateAsync(TModel updated)
        {
            if (updated == null)
            {
                return null;
            }

            var existing = await GetByIdAsync(updated.Id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                await SaveChangesAsync();
            }

            return existing;
        }

        //public virtual TModel UpdateOrAdd(TModel model)
        //{
        //    if (model == null)
        //    {
        //        return null;
        //    }

        //    var existing = await GetByIdAsync(model.Id);

        //    if (existing != null)
        //        Context.Entry(existing).CurrentValues.SetValues(model);
        //    else
        //        Context.Set<TModel>().Add(model);

        //    SaveChanges();


        //    return existing;
        //}


        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

        //TODO: optimize the # of calls the DB.
        public virtual async Task DeleteAsync(IList<TId> ids)
        {
            foreach (var id in ids)
            {
                var entity = await GetByIdAsync(id);
                DbSet.Remove(entity);
            }

            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            Context.Set<TModel>().Remove(model);
            SaveChanges();
        }

        public virtual void SaveChanges() => Context.SaveChanges();
        public virtual async Task SaveChangesAsync() => await Context.SaveChangesAsync();

    }
}
