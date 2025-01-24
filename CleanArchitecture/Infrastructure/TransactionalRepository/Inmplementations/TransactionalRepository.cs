using Application.Common;
using Infrastructure.TransactionalRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Inmplementations
{
    public abstract class TransactionalRepository : ITransactionalRepository
    {
        protected readonly ILogger<TransactionalRepository> _logger;

        protected readonly DbContext context;

        protected TransactionalRepository(ITransactionalConfiguration configuration)
        {
            _logger = configuration.Logger;
            context = configuration.GetContext<DbContext>();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                context.Remove(entity);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to delete a record");
                throw;
            }
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                context.RemoveRange(entities);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to delete a record");
                throw;
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                context.Add(entity);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to add a record");
                throw;
            }
        }

        public void InsertRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                context.AddRange(entities);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to add a record");
                throw;
            }
        }

        public void SaveChange()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to persist in the database");
                throw;
            }
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to persist in the database");
                throw;
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                context.Attach(entity).State = EntityState.Modified;
                context.Update(entity);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to update a record");
                throw;
            }
        }

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                context.AttachRange(entities);
                context.UpdateRange(entities);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to update a record");
                throw;
            }
        }

        public TEntity FindById<TEntity>(dynamic id) where TEntity : class
        {
            try
            {
                return context.Find<TEntity>(id);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to find a record");
                throw;
            }
        }

        [Obsolete("Use context instead")]
        public void UseContext(Action<DbContext> action)
        {
            action(context);
        }

        [Obsolete("Use context instead")]
        public TResult UseContext<TResult>(Func<DbContext, TResult> action) where TResult : class
        {
            return action(context);
        }
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            try
            {
                return context.Set<TEntity>().ToList();

            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to find a record");
                throw;
            }
        }

        public IEnumerable<TEntity> GetByCondition<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            try
            {
                return context.Set<TEntity>().Where(expression).ToList();

            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to find a record");
                throw;
            }
        }

        public async Task<TEntity?> GetByConditionAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeMethod = null,
        CancellationToken cancellationToken = default
    ) where TEntity : class
        {
            try
            {
                var query = context.Set<TEntity>().Where(expression);

                if (includeMethod != null)
                    query = includeMethod(query); // Aplica los includes

                return await query.FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar registros");
                throw;
            }
        }
    }
}
