namespace VideoHosting.Core.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(string id);
    Task<TEntity> UpdateAsync(TEntity entity, string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteIfExistsAsync(string id);
}