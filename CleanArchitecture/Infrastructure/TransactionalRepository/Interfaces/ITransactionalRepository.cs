using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Interfaces
{
    public interface ITransactionalRepository
    {
        void Insert<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void InsertRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        [Obsolete("Use context instead")]
        void UseContext(Action<DbContext> action);

        [Obsolete("Use context instead")]
        TResult UseContext<TResult>(Func<DbContext, TResult> action) where TResult : class;

        Task SaveChangeAsync();

        void SaveChange();

        TEntity FindById<TEntity>(dynamic id) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        IEnumerable<TEntity> GetByCondition<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    }
}
