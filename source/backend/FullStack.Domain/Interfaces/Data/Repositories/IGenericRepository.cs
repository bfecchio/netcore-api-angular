using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Interfaces.Data.Repositories
{
    public interface IGenericRepository<TEntity, TKey> : IBaseRepository
        where TEntity : class, IBaseEntity
    {
        #region IGenericRepository Members

        Task<TEntity> Get(TKey id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(
              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        );

        Task<TEntity> Find(
              Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        );

        Task<IEnumerable<TEntity>> FindAll(
              Expression<Func<TEntity, bool>> predicate
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        );

        Task<IPagedListEntity<TEntity>> PagedList(
              int pageIndex
            , int pageSize
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        );

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TKey id);

        Task Delete(TEntity entity);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<TKey> Min(Expression<Func<TEntity, TKey>> selector, Expression<Func<TEntity, bool>> predicate = null);

        Task<TKey> Max(Expression<Func<TEntity, TKey>> selector, Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }
}
