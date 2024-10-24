using JetBrains.Annotations;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using tnine.Core.Shared.Domain.Entities;

namespace tnine.Core.Shared.Domain.Repositories
{
    public interface IRepository
    {
        bool? IsChangeTrackingEnabled { get; }
    }
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>, IBasicRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> FindAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
            );
        Task<TEntity> GetAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
            );
        Task DeleteAsync(
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            bool autoSave = false,
            CancellationToken cancellationToken = default
            );
        Task DeleteDirectAsync(
            [NotNull] TEntity entity,
            bool autoSave = false,
            CancellationToken cancellationToken = default
            );
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IReadOnlyRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    {
    }
}
