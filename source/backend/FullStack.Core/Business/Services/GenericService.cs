using System;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Query;
using FullStack.Domain.Interfaces.Entities;
using FullStack.Domain.Interfaces.Business.Services;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Business.Services
{
    public abstract class GenericService<TEntity, TKey, TValidator, TRepository> : IGenericService<TEntity, TKey, TValidator, TRepository>
        where TEntity : class, IBaseEntity
        where TValidator : class, IValidator<TEntity>
        where TRepository : class, IGenericRepository<TEntity, TKey>
    {
        #region Protected Read-Only Fields

        protected readonly ILogger _logger;
        protected readonly TValidator _validator;
        protected readonly TRepository _repository;

        #endregion

        #region Constructors

        protected GenericService(ILogger logger, TValidator validator, TRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region IGenericService Members

        public virtual async Task<TEntity> Get(TKey id)
            => await _repository.Get(id);

        public virtual async Task<IEnumerable<TEntity>> GetAll()
            => await _repository.GetAll();

        public virtual async Task Create(TEntity entity)
        {
            await _validator.ValidateAndThrowAsync(entity);
            await _repository.Create(entity);
        }

        public virtual async Task Update(TEntity entity)
        {
            var parsed = TryParseKey(entity, out TKey id);
            if (!parsed) throw new InvalidCastException();

            var existing = await Get(id);
            if (existing is null)
                throw new ArgumentException(nameof(existing));

            await _validator.ValidateAndThrowAsync(entity);
            await _repository.Update(entity);
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await Get(id);
            if (entity is null)
                throw new ArgumentException(nameof(entity));

            await _repository.Delete(id);
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var parsed = TryParseKey(entity, out TKey id);
            await (parsed ? Delete(id) : _repository.Delete(entity));
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
            => await _repository.Any(predicate);

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
            => await _repository.Count(predicate);

        public virtual async Task<IPagedListEntity<TEntity>> PagedList(
              int pageIndex
            , int pageSize
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        )
            => await _repository.PagedList(pageIndex, pageSize, predicate, orderBy, include, disableTracking);

        public virtual async Task<TKey> Min(Expression<Func<TEntity, TKey>> selector, Expression<Func<TEntity, bool>> predicate = null)
            => await _repository.Min(selector, predicate);

        public virtual async Task<TKey> Max(Expression<Func<TEntity, TKey>> selector, Expression<Func<TEntity, bool>> predicate = null)
            => await _repository.Max(selector, predicate);

        #endregion

        #region Private Methods

        private bool TryParseKey(TEntity entity, out TKey key)
        {
            key = default;

            if (typeof(IManagedEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                key = ((IManagedEntity<TKey>)entity).Id;
                return true;
            }
            else if (typeof(ITrackableEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                key = ((ITrackableEntity<TKey>)entity).Id;
                return true;
            }
            else if (typeof(IEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                key = ((IEntity<TKey>)entity).Id;
                return true;
            }

            return false;
        }

        #endregion
    }
}
