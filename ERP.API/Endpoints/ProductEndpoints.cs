using ERP.Domain;
using ERP.Domain.Shared;
using ERP.Domain.Shared.ValueObjects;
using ERP.Infrastructure.Queries;

namespace ERP.API.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/", GetProducts);
        group.MapGet("/{id}", GetProduct);
        group.MapPost("/", RegisterProduct);
        group.MapPatch("/{id}", UpdateProductInformation);
        group.MapDelete("/{id}", DeleteProduct);
    }

    private record RegisterProductRequest(string Name, string Description, MoneyRequest Price);
    private record MoneyRequest(decimal Amount, string Currency);
    private record UpdateProductInformationRequest(string Name, string Description, MoneyRequest Price);

    private static async Task<IProductQueries.ProductResponse[]> GetProducts(IProductQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetProductsAsync(cancellationToken);

    private static async Task<IResult> GetProduct(Guid id, IProductQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetProductAsync(new Product.ProductId(id), cancellationToken) is IProductQueries.ProductResponse product
        ? Results.Ok(product)
        : Results.NotFound();

    private static async Task<IResult> RegisterProduct(RegisterProductRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var product = Product.Register(
            request.Name,
            request.Description,
            new Money(
                request.Price.Amount,
                request.Price.Currency
            )
        );

        unitOfWork.GetRepository<Product, Product.ProductId>().Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.Created($"/products/{product.PublicId}", product.PublicId.Value);
    }

    private static async Task<IResult> UpdateProductInformation(Guid id, UpdateProductInformationRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.GetRepository<Product, Product.ProductId>().TryFindAsync(new Product.ProductId(id), cancellationToken);

        if (product == null) return Results.NotFound();

        product.Rename(request.Name);
        product.ChangeDescription(request.Description);
        product.ChangePrice(new(
            request.Price.Amount,
            request.Price.Currency
        ));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteProduct(Guid id, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        await unitOfWork.GetRepository<Product, Product.ProductId>().Delete(new Product.ProductId(id), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}