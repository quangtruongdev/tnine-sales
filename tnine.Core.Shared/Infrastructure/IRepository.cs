using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace tnine.Core.Shared.Infrastructure
{
    public interface IRepository<TEntity, TKey> where TEntity : class where TKey : struct
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity Delete(TKey id);
        void DeleteMulti(Expression<Func<TEntity, bool>> where);
        TEntity GetSingleById(TKey id);
        TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> expression, string[] includes = null);
        Task<List<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(string[] includes = null);
        IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);
        int Count(Expression<Func<TEntity, bool>> where);
        bool CheckContains(Expression<Func<TEntity, bool>> predicate);
    }
}
