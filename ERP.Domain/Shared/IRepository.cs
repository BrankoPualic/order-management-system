namespace ERP.Domain.Shared;

public interface IRepository<TAggregate, TKey>
{
    Task<TAggregate?> TryFindAsync(TKey key);
    void Add(TAggregate aggregate);
    void Delete(TAggregate aggregate);
    async Task Delete(TKey key)
    {
        var aggregate = await TryFindAsync(key) ?? throw new KeyNotFoundException("Key not found");
        Delete(aggregate);
    }
}