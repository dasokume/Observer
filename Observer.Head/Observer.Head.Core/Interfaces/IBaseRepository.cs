using System.Linq.Expressions;

namespace Observer.Head.Core.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(string id);
    Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> UpdateAsync(TEntity entity, string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteIfExistsAsync(string id);
}