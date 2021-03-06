﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FullStack.Domain.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using FullStack.Domain.Interfaces.Entities;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Data.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity
    {
        #region Protected Read-Only Fields

        protected readonly ILogger _logger;
        protected readonly EFContext _dbContext;

        #endregion

        #region Constructors

        protected GenericRepository(EFContext dbContext, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion

        #region IGenericRepository Members

        public virtual async Task<TEntity> Get(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetAll()
            => await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAll(
              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        )
        {
            var query = _dbContext.Set<TEntity>()
                .AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> Find(
              Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        )
        {
            var query = _dbContext.Set<TEntity>()
                .AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll(
              Expression<Func<TEntity, bool>> predicate
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        )
        {
            var query = _dbContext.Set<TEntity>()
                .AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<IPagedListEntity<TEntity>> PagedList(
              int pageIndex
            , int pageSize
            , Expression<Func<TEntity, bool>> predicate
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
        )
        {
            var query = _dbContext.Set<TEntity>()
                .AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            var output = new PagedListEntity<TEntity>(pageIndex, pageSize);
            output.Total = await query.CountAsync();

            if (orderBy != null)
                output.Collection = await orderBy(query)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync();
            else
                output.Collection = await query
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return output;
        }

        public virtual async Task Create(TEntity entity)
        {
            if (typeof(IManagedEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                ((IManagedEntity<TKey>)entity).CreatedDate = DateTime.Now;
            else if (typeof(ITrackableEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                ((ITrackableEntity<TKey>)entity).CreatedDate = DateTime.Now;

            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity)
        {
            TKey id;

            if (typeof(IManagedEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((IManagedEntity<TKey>)entity).Id;
            else if (typeof(ITrackableEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((ITrackableEntity<TKey>)entity).Id;
            else if (typeof(IEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((IEntity<TKey>)entity).Id;
            else
                throw new InvalidCastException();

            var existing = await Get(id);

            if (existing != null)
            {
                if (typeof(IManagedEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    ((IManagedEntity<TKey>)entity).CreatedBy = ((IManagedEntity<TKey>)existing).CreatedBy;
                    ((IManagedEntity<TKey>)entity).CreatedDate = ((IManagedEntity<TKey>)existing).CreatedDate;
                }
                else if (typeof(ITrackableEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    ((ITrackableEntity<TKey>)entity).CreatedBy = ((ITrackableEntity<TKey>)existing).CreatedBy;
                    ((ITrackableEntity<TKey>)entity).CreatedDate = ((ITrackableEntity<TKey>)existing).CreatedDate;
                    ((ITrackableEntity<TKey>)entity).ModifiedDate = DateTime.Now;
                }

                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            }
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await Get(id);
            if (entity != null) _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var id = default(TKey);

            if (typeof(IManagedEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((IManagedEntity<TKey>)entity).Id;
            else if (typeof(ITrackableEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((ITrackableEntity<TKey>)entity).Id;
            else if (typeof(IEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
                id = ((IEntity<TKey>)entity).Id;

            if (id.Equals(default(TKey)))
                _dbContext.Set<TEntity>().Remove(entity);
            else
                await Delete(id);
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
            => await _dbContext.Set<TEntity>().AnyAsync(predicate);

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            try
            {
                var query = _dbContext.Set<TEntity>().AsQueryable();
                if (predicate != null) query = query.Where(predicate);

                return await query.CountAsync();
            }
            catch (InvalidOperationException) { return await Task.FromResult(0); }
        }

        public virtual async Task<TKey> Min(
              Expression<Func<TEntity, TKey>> selector
            , Expression<Func<TEntity, bool>> predicate = null
        )
        {
            try
            {
                var query = _dbContext.Set<TEntity>().AsQueryable();
                if (predicate != null) query = query.Where(predicate);

                return await query.MinAsync(selector);
            }
            catch (InvalidOperationException) { return await Task.FromResult<TKey>(default); }
        }

        public virtual async Task<TKey> Max(
              Expression<Func<TEntity, TKey>> selector
            , Expression<Func<TEntity, bool>> predicate = null
        )
        {
            try
            {
                var query = _dbContext.Set<TEntity>().AsQueryable();
                if (predicate != null) query = query.Where(predicate);

                return await query.MaxAsync(selector);
            }
            catch (InvalidOperationException) { return await Task.FromResult<TKey>(default); }
        }

        #endregion
    }
}
