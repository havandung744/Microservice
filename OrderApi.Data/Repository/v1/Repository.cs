﻿using Microsoft.AspNetCore.Mvc;
using OrderApi.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly OrderContext OrderContext;

        public Repository(OrderContext orderContext)
        {
            OrderContext = orderContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await OrderContext.AddAsync(entity);
                await OrderContext.SaveChangesAsync();

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
                return OrderContext.Set<TEntity>();
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
                OrderContext.Update(entity);
                await OrderContext.SaveChangesAsync();

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
                OrderContext.UpdateRange(entities);
                await OrderContext.SaveChangesAsync();
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
                var order = await OrderContext.Order.FindAsync(guilId);
                if (order != null)
                {
                    OrderContext.Remove(order);
                    await OrderContext.SaveChangesAsync();
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
