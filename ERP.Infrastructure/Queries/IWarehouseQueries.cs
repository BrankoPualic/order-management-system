using ERP.Domain;
using ERP.Infrastructure.Queries.Responses;

namespace ERP.Infrastructure.Queries;

public interface IWarehouseQueries
{
    public record WarehouseResponse(Guid Id, string Name, string Description, DateTime CreatedOn, AddressResponse Address);

    Task<WarehouseResponse[]> GetWarehousesAsync(CancellationToken cancellationToken = default);
    Task<WarehouseResponse?> GetWarehouseAsync(Warehouse.WarehouseId id, CancellationToken cancellationToken = default);
}