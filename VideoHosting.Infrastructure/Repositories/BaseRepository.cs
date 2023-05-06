﻿using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly CosmosDbContext _cosmosDbContext;
    protected readonly string _partitionKeyValue;

    public BaseRepository(CosmosDbContext cosmosDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _partitionKeyValue = string.Empty;
    }

    public BaseRepository(CosmosDbContext cosmosDbContext, string partitionKeyValue)
    {
        _cosmosDbContext = cosmosDbContext;
        _partitionKeyValue = partitionKeyValue;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        return await _cosmosDbContext.CreateAsync(entity, _partitionKeyValue);
    }

    public async Task<TEntity> GetAsync(string id)
    {
        return await _cosmosDbContext.ReadAsync<TEntity>(id, _partitionKeyValue);
    }

    public async Task<TEntity> UpdateAsync(TEntity item, string id)
    {
        return await _cosmosDbContext.UpdateAsync(item, id, _partitionKeyValue);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _cosmosDbContext.DeleteAsync<TEntity>(id, _partitionKeyValue);
    }

    public async Task<bool> DeleteIfExistsAsync(string id)
    {
        return await _cosmosDbContext.DeleteIfExistsAsync<TEntity>(id, _partitionKeyValue);
    }
}