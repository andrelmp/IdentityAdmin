using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityAdmin.Core.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        Task<OperationResult> Add(TEntity entity);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Delete(TEntity entity);
        Task<TEntity> GetByKey(TKey key);
        Task<TEntity> GetById(string id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
