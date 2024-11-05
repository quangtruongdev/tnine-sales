using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace tnine.Core.Shared.Infrastructure
{
    public interface IRepository<TEntity, TKey> where TEntity : class where TKey : struct
    {
        #region CREATE
        TEntity Add(TEntity entity);
        #endregion

        #region CREATE ASYNC
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TKey> InsertAndGetIdAsync(TEntity entity);
        #endregion

        #region UPDATE
        void Update(TEntity entity);
        #endregion

        #region UPDATE ASYNC
        Task<TEntity> UpdateAsync(TEntity entity);
        #endregion

        #region DELETE
        TEntity Delete(TEntity entity);
        TEntity Delete(TKey id);
        void DeleteMulti(Expression<Func<TEntity, bool>> where);

        #endregion

        #region DELETE ASYNC
        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        //Task DeleteMultiAsync(Expression<Func<TEntity, bool>> where);
        #endregion

        #region READ
        TEntity GetSingleById(TKey id);
        TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> expression, string[] includes = null);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(string[] includes = null);
        IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);
        int Count(Expression<Func<TEntity, bool>> where);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);
        bool CheckContains(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region READ ASYNC
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleByIdAsync(TKey id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);

        #endregion

    }
}
