using Microsoft.EntityFrameworkCore;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class BaseRepository<TEntity>(SmartShoppingAssistantDbContext context) : IRepository<TEntity> where TEntity : class
    {
        // task e o operatie asincrona care returneaza o valoare


        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity with id {id} not found.");
                }

                return entity;
            }
            catch (Exception ex) { 
                throw new Exception($"Error retrieving entity by id {id} : {ex.Message}", ex);
            }
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                var entities = await context.Set<TEntity>().ToListAsync();

                if(!entities.Any())
                {
                    throw new KeyNotFoundException("No entities found.");
                }

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all entities: {ex.Message}", ex);
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                // Adaugam entitatea in context
                await context.Set<TEntity>().AddAsync(entity);
                // Salvam modificarile in baza de date
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            // operatia de update e async by default
            context.Set<TEntity>().Update(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity with id {id} not found.");
                }

                context.Set<TEntity>().Remove(entity);

                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity by id {id} : {ex.Message}", ex);
            }
        }

    }
}
