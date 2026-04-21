namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class BaseRepository<TEntity>(SmartShoppingAssistantDbContext context) : IRepository<TEntity> where TEntity : class
    {
        // task e o operatie asincrona care returneaza o valoare


        public async Task<TEntity> GetByIdAsync(int id)
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

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> DeleteAsync(int id)
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
