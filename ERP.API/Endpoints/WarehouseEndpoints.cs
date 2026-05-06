using ERP.API.Endpoints.Request;
using ERP.Domain;
using ERP.Domain.Shared;
using ERP.Domain.Shared.ValueObjects;
using ERP.Infrastructure.Queries;

namespace ERP.API.Endpoints;

public static class WarehouseEndpoints
{
    public static void MapWarehouseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/warehouses");

        group.MapGet("/", GetWarehouses);
        group.MapGet("/{id}", GetWarehouse);
        group.MapPost("/", RegisterWarehouse);
        group.MapPatch("/{id}", UpdateWarehouseInformation);
        group.MapDelete("/{id}", DeleteWarehouse);
    }

    private record RegisterWarehouseRequest(string Name, string Description, AddressRequest Address);
    private record UpdateWarehouseInformationRequest(string Name, string Description, AddressRequest Address);

    private static async Task<IWarehouseQueries.WarehouseResponse[]> GetWarehouses(IWarehouseQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetWarehousesAsync(cancellationToken);

    private static async Task<IResult> GetWarehouse(Guid id, IWarehouseQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetWarehouseAsync(new Warehouse.WarehouseId(id), cancellationToken) is IWarehouseQueries.WarehouseResponse company
        ? Results.Ok(company)
        : Results.NotFound();

    private static async Task<IResult> RegisterWarehouse(RegisterWarehouseRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var warehouse = Warehouse.Register(
            request.Name,
            request.Description,
            new Address(
                request.Address.Street,
                request.Address.City,
                request.Address.State,
                request.Address.Country,
                request.Address.ZipCode
            )
        );

        unitOfWork.GetRepository<Warehouse, Warehouse.WarehouseId>().Add(warehouse);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.Created($"/warehouses/{warehouse.PublicId}", warehouse.PublicId.Value);
    }

    private static async Task<IResult> UpdateWarehouseInformation(Guid id, UpdateWarehouseInformationRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var warehouse = await unitOfWork.GetRepository<Warehouse, Warehouse.WarehouseId>().TryFindAsync(new Warehouse.WarehouseId(id), cancellationToken);

        if (warehouse == null) return Results.NotFound();

        warehouse.Rename(request.Name);
        warehouse.ChangeDescription(request.Description);
        warehouse.ChangeAddress(new(
            request.Address.Street,
            request.Address.City,
            request.Address.State,
            request.Address.Country,
            request.Address.ZipCode
        ));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteWarehouse(Guid id, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        await unitOfWork.GetRepository<Warehouse, Warehouse.WarehouseId>().Delete(new Warehouse.WarehouseId(id), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}