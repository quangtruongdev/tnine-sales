using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using tnine.Core.Shared.Domain.Entities;

namespace tnine.Core.Shared.Domain.Repositories
{
    public interface IReadOnlyBasicRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetPagedListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            );
    }

    public interface IReadOnlyBasicRepository<TEntity, TKey> : IReadOnlyBasicRepository<TEntity>
    where TEntity : class, IEntity<TKey>
    {
        //Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
        //Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}