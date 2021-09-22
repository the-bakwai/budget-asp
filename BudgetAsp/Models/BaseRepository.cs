using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BudgetAsp.Models
{
    public abstract class BaseRepository<TEntity>: IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbContext Context { get; set; }


        public async Task<TEntity> Get(long id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllActiveOnly()
        {
            return await Context.Set<TEntity>().Where(e => e.Deleted == false).ToListAsync();
        }

        public async Task Save(TEntity entity)
        {
            if (entity.Id == 0)
            {
                entity.DateCreated = DateTime.Now;
                entity.DateUpdated = entity.DateCreated;
                
                await Context.Set<TEntity>().AddAsync(entity);
            }
            else
            {
                entity.DateUpdated = DateTime.Now;
                Context.Entry(entity).State = EntityState.Modified;
            }

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                entity.Deleted = true;
                await Context.SaveChangesAsync();
            }
        }
    }
}