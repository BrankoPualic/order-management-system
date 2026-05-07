using ERP.Domain;

namespace ERP.Infrastructure.Queries;

public interface IProductQueries
{
    public record ProductResponse(Guid Id, string Name, string Description, DateTime CreatedOn, MoneyResponse Price);
    public record MoneyResponse(decimal Amount, CurrencyResponse Currency);
    public record CurrencyResponse(string Code);

    Task<ProductResponse[]> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductResponse?> GetProductAsync(Product.ProductId id, CancellationToken cancellationToken = default);
}