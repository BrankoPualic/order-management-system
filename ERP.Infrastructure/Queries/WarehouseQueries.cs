using ERP.Domain;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Queries.Responses;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Queries;

public class WarehouseQueries(ApplicationDbContext dbContext) : IWarehouseQueries
{
    public async Task<IWarehouseQueries.WarehouseResponse[]> GetWarehousesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Warehouses
        .Select(warehouse => new IWarehouseQueries.WarehouseResponse(
            warehouse.PublicId.Value,
            warehouse.Name,
            warehouse.Description,
            warehouse.CreatedOn,
            new AddressResponse(
                warehouse.Address.Street,
                warehouse.Address.City,
                warehouse.Address.State,
                warehouse.Address.Country,
                warehouse.Address.ZipCode
            )
        ))
        .AsNoTracking()
        .ToArrayAsync(cancellationToken);

    public async Task<IWarehouseQueries.WarehouseResponse?> GetWarehouseAsync(Warehouse.WarehouseId id, CancellationToken cancellationToken = default) =>
        await dbContext.Warehouses
        .Where(warehouse => warehouse.PublicId == id)
        .Select(warehouse => new IWarehouseQueries.WarehouseResponse(
            warehouse.PublicId.Value,
            warehouse.Name,
            warehouse.Description,
            warehouse.CreatedOn,
            new AddressResponse(
                warehouse.Address.Street,
                warehouse.Address.City,
                warehouse.Address.State,
                warehouse.Address.Country,
                warehouse.Address.ZipCode
            )
        ))
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
}