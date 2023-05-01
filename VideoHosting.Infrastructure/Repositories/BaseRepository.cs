using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly CosmosDbContext _cosmosDbContext;

    public BaseRepository(CosmosDbContext cosmosDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
    }

    public async Task<T> CreateItemAsync(T item, string id)
    {
        return await _cosmosDbContext.CreateItemAsync(item, id);
    }

    public async Task<T> GetItemAsync(string id)
    {
        return await _cosmosDbContext.ReadItemAsync<T>(id, id);
    }

    public async Task<T> UpdateItemAsync(T item, string id)
    {
        return await _cosmosDbContext.UpdateItemAsync(item, id, id);
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
        return await _cosmosDbContext.DeleteItemAsync<T>(id, id);
    }

    public async Task<bool> DeleteItemIfExistsAsync(string id)
    {
        return await _cosmosDbContext.DeleteItemIfExistsAsync<T>(id, id);
    }
}