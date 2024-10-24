using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using tnine.Core.Auditing;

namespace tnine.Core.Shared.Infrastructure
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : FullAuditedEntity<TKey> where TKey : struct
    {
        private DatabaseContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        protected IDbFactory DbFactory { get; private set; }

        protected DatabaseContext DbContext => _dbContext ?? (_dbContext = DbFactory.Init());

        protected Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            return entity;
        }

        #region CREATE ASYNC

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }


        #endregion

        #region UPDATE
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region UPDATE ASYNC

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region DELETE
        public virtual TEntity Delete(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return _dbSet.Remove(entity);
        }

        public virtual TEntity Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            return Delete(entity);
        }

        public virtual void DeleteMulti(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = _dbSet.Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                _dbContext.Entry(obj).State = EntityState.Deleted;
                _dbSet.Remove(obj);
            }
        }

        #endregion

        #region DELETE ASYNC

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            await DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        public virtual TEntity GetSingleById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.FirstOrDefault(expression);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll(string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.AsEnumerable();
            }
            return _dbSet.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.Where(predicate).AsEnumerable();
            }
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<TEntity> query = _dbSet;

            if (includes != null && includes.Count() > 0)
            {
                var query1 = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query1 = query1.Include(include);
                }
                query = query1;
            }

            query = skipCount == 0 ? query.Take(size) : query.Skip(skipCount).Take(size);
            total = query.Count();
            return query.Where(filter).AsEnumerable();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Count(where);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.CountAsync(where);
        }

        public bool CheckContains(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate) > 0;
        }

        #region READ ASYNC
        public virtual async Task<TEntity> GetSingleByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet.ToListAsync(cancellationToken);
        //}

        //public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet.LongCountAsync(cancellationToken);
        //}

        //public async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet.Skip(skipCount).Take(maxResultCount).ToListAsync(cancellationToken);
        //}

        #endregion
    }
}
