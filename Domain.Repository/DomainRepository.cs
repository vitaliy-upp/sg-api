using Common.DataAccess.Utilities;
using Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class DomainRepository<TModel, TId> : IDomainService<TModel, TId> where TModel : class, IBaseDomainModel<TId>
    {
        protected readonly DomainDbContext Context;

        protected DomainRepository(DomainDbContext dbContext)
        {
            Context = dbContext;
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

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            return await Context.Set<TModel>().ToListAsync();
        }
        public virtual int GetCount()
        {
            return Context.Set<TModel>().Count();
        }
        public virtual TModel Create(TModel model)
        {
            Context.Set<TModel>().Add(model);
            //SaveChanges();
            return model;
        }

        public virtual async Task<TModel> CreateAsync(TModel model, bool? shouldSaveChanges = false)
        {
            await Context.Set<TModel>().AddAsync(model);
            if (shouldSaveChanges.HasValue && shouldSaveChanges.Value)
            {
                await SaveChangesAsync();
            }
            return model;
        }

        public virtual async Task<TModel> UpdateAsync(TModel updated)
        {
            if (updated == null)
            {
                return null;
            }


            Context.Attach(updated);
            //var existing = await GetByIdAsync(updated.Id);
            //if (existing != null)
            //{
            //    Context.Entry(existing).CurrentValues.SetValues(updated);
            //    //SaveChanges();
            //}

            return updated;
        }

        public virtual TModel UpdateOrAdd(TModel model)
        {
            if (model == null)
            {
                return null;
            }

            var existing = GetByIdAsync(model.Id).Result;

            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(model);
            }
            else
            {
                Context.Set<TModel>().Add(model);
            }

            //SaveChanges();


            return existing;
        }


        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            DbSet.Remove(entity);
            //SaveChanges();
        }

        public virtual async Task DeleteAsync(IList<TId> ids)
        {
            foreach (var id in ids)
            {
                var entity = await GetByIdAsync(id);
                DbSet.Remove(entity);
            }

            //SaveChanges();
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            Context.Set<TModel>().Remove(model);
            //SaveChanges();
        }

        public virtual void SaveChanges() => Context.SaveChanges();
        public virtual async Task SaveChangesAsync() => await Context.SaveChangesAsync();

    }
}
