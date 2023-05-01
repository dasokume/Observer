namespace VideoHosting.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateItemAsync(T item, string id);
    Task<T> GetItemAsync(string id);
    Task<T> UpdateItemAsync(T item, string id);
    Task<bool> DeleteItemAsync(string id);
    Task<bool> DeleteItemIfExistsAsync(string id);
}