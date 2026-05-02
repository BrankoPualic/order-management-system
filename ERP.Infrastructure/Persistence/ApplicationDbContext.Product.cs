using ERP.Domain;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Persistence;

public partial class ApplicationDbContext : IProductRepository
{
    public DbSet<Product> Products => Set<Product>();

    public async Task<Product?> TryFindAsync(Product.ProductId key, CancellationToken cancellationToken = default) => await Products.FirstOrDefaultAsync(_ => _.PublicId == key, cancellationToken);
    public void Add(Product aggregate) => Products.Add(aggregate);
    public void Delete(Product aggregate) => Products.Remove(aggregate);
}