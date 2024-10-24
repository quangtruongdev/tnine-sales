using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using tnine.Core.Shared.Domain.Entities;

namespace tnine.Core.Shared.Domain.Repositories
{
    public interface IBasicRepository<TEntity> : IReadOnlyBasicRepository<TEntity> where TEntity : class, IEntity
    {
        [NotNull]
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        [NotNull]
        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
    }

    public interface IBasicRepository<TEntity, TKey> : IBasicRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteManyAsync([NotNull] IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default);

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        TKey InsertAndGetId(TEntity entity);
        Task<TKey> InsertAndGetIdAsync(TEntity entity);
        TEntity InsertOrUpdate(TEntity entity);
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        TKey InsertOrUpdateAndGetId(TEntity entity);
        Task<TKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(TKey id);
        Task DeleteAsync(TKey id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    }
}