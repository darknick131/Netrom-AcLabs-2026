using Microsoft.EntityFrameworkCore;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class BaseRepository<TEntity>(SmartShoppingAssistantDbContext context) : IRepository<TEntity> where TEntity : class
    {
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            return entity;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            // Lista goala e un raspuns valid (200 []), nu o eroare
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
