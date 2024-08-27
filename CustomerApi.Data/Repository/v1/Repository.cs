using CustomerApi.Data.Database;

namespace CustomerApi.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly CustomerContext CustomerContext;

        public Repository(CustomerContext customerContext)
        {
            CustomerContext = customerContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await CustomerContext.AddAsync(entity);
                await CustomerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved {ex.Message}");
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return CustomerContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                CustomerContext.Update(entity);
                await CustomerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateRangeAsync)} entities must not be null");
            }

            try
            {
                CustomerContext.UpdateRange(entities);
                await CustomerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be updated {ex.Message}");
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} id must not be null");
            }
            try
            {
                var guilId = Guid.Parse(id);
                var order = await CustomerContext.Customer.FindAsync(guilId);
                if (order != null)
                {
                    CustomerContext.Remove(order);
                    await CustomerContext.SaveChangesAsync();
                }

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(id)} could not be updated {ex.Message}");
            }
        }
    }
}
