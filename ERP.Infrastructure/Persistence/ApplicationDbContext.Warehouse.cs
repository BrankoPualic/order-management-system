using ERP.Domain;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Persistence;

public partial class ApplicationDbContext : IWarehouseRepository
{
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public async Task<Warehouse?> TryFindAsync(Warehouse.WarehouseId key, CancellationToken cancellationToken = default) => await Warehouses.FirstOrDefaultAsync(_ => _.PublicId == key, cancellationToken);
    public void Add(Warehouse aggregate) => Warehouses.Add(aggregate);
    public void Delete(Warehouse aggregate) => Warehouses.Remove(aggregate);
}