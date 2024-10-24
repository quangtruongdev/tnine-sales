using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using tnine.Core.Shared.Domain.Entities;
using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : struct
    {
        private DatabaseContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public bool? IsChangeTrackingEnabled { get; }

        protected IDbFactory DbFactory { get; private set; }
        protected DatabaseContext DbContext => _dbContext ?? (_dbContext = DbFactory.Init());

        public RepositoryBase(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(predicate);
            if (includeDetails)
            {
                query = query.Include("RelatedEntity");
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(predicate);
            if (includeDetails)
            {
                query = query.Include("RelatedEntity");
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteDirectAsync(
            [NotNull] TEntity entity,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        #region Querying
        #region Getting single entity

        public TEntity Get(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleAsync(predicate);
        }

        public TEntity FirstOrDefault(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity Load(TKey id)
        {
            return _dbSet.Find(id);
        }

        #endregion

        #region Getting a list of entities
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = _dbSet.AsQueryable();
            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }

        public List<TEntity> GetAllList()
        {
            return _dbSet.ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        #endregion

        #endregion

        #region Insert

        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(_dbSet.Add(entity));
        }

        public TKey InsertAndGetId(TEntity entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            return insertedEntity.Id;
        }

        public async Task<TKey> InsertAndGetIdAsync(TEntity entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            return await Task.FromResult(insertedEntity.Id);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_dbSet.Add(entity));
        }

        public TKey InsertOrUpdateAndGetId(TEntity entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            return insertedEntity.Id;
        }

        public async Task<TKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            return await Task.FromResult(insertedEntity.Id);
        }

        #endregion

        #region Update

        public TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            return await Task.FromResult(entity);
        }

        #endregion

        #region Delete

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public void Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate).ToList();
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate).ToList();
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }

            await Task.CompletedTask;
        }

        #endregion

        #region IBasicRepository

        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteManyAsync([NotNull] IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
            if (!entities.Any())
            {
                return;
            }

            _dbSet.RemoveRange(entities);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public async Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.AddRange(entities);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public async Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        #endregion

        #region IReadOnlBasicRepository

        public async Task<List<TEntity>> GetListAsync(bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (includeDetails)
            {
                query = query.Include("RelatedEntity");
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.LongCountAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetPagedListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (includeDetails)
            {
                query = query.Include("RelatedEntity");
            }

            // Assuming sorting is a property name of TEntity
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, sorting);
            var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

            return await query.OrderBy(lambda)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(cancellationToken);
        }

        #endregion
    }
}
