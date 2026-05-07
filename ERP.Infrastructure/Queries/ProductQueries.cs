using ERP.Domain;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Queries;

public class ProductQueries(ApplicationDbContext dbContext) : IProductQueries
{
    public async Task<IProductQueries.ProductResponse[]> GetProductsAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Products
        .Select(product => new IProductQueries.ProductResponse(
            product.PublicId.Value,
            product.Name,
            product.Description,
            product.CreatedOn,
            new IProductQueries.MoneyResponse(
                product.Price.Amount,
                new IProductQueries.CurrencyResponse(product.Price.Currency.Code)
            )
        ))
        .AsNoTracking()
        .ToArrayAsync(cancellationToken);

    public async Task<IProductQueries.ProductResponse?> GetProductAsync(Product.ProductId id, CancellationToken cancellationToken = default) =>
        await dbContext.Products
        .Where(product => product.PublicId == id)
        .Select(product => new IProductQueries.ProductResponse(
            product.PublicId.Value,
            product.Name,
            product.Description,
            product.CreatedOn,
            new IProductQueries.MoneyResponse(
                product.Price.Amount,
                new IProductQueries.CurrencyResponse(product.Price.Currency.Code)
            ))
        )
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
}