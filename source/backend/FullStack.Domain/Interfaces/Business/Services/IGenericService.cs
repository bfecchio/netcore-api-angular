using System;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
using FullStack.Domain.Interfaces.Entities;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Domain.Interfaces.Business.Services
{
    public interface IGenericService<TEntity, TKey, TValidator, TRepository> : IBaseService
        where TEntity : class, IBaseEntity
        where TValidator : class, IValidator<TEntity>
        where TRepository : class, IGenericRepository<TEntity, TKey>
    {
        #region IGenericService Members

        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
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
