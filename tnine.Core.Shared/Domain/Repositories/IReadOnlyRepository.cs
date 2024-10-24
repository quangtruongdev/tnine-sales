using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tnine.Core.Shared.Domain.Entities;

namespace tnine.Core.Shared.Domain.Repositories
{
    public interface IReadOnlyRepository<TEntity> : IReadOnlyBasicRepository<TEntity> where TEntity : class, IEntity
    {
        //IAsyncQueryableExecuter AsyncExecuter { get; }

        //[Obsolete("Use WithDetailsAsync method.")]
        //IQueryable<TEntity> WithDetails();

        //[Obsolete("Use WithDetailsAsync method.")]
        //IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);

        //Task<IQueryable<TEntity>> WithDetailsAsync();

        //Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors);

        //Task<IQueryable<TEntity>> GetQueryableAsync();
        //Task<List<TEntity>> GetListAsync(
        //    [NotNull] Expression<Func<TEntity, bool>> predicate,
        //    bool includeDetails = false,
        //    CancellationToken cancellationToken = default);
    }

    public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(TKey id);
        Task<TEntity> FirstOrDefaultAsync(TKey id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Load(TKey id);
        List<TEntity> GetAllList();
        Task<List<TEntity>> GetAllListAsync();
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
    }
}