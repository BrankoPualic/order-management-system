using ERP.Domain;
using ERP.Domain.Shared;
using ERP.Domain.Shared.ValueObjects;
using ERP.Infrastructure.Queries;

namespace ERP.API.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/companies");

        group.MapGet("/", GetCompanies);
        group.MapGet("/{id}", GetCompany);
        group.MapPost("/", RegisterCompany);
        group.MapPatch("/{id}/name", RenameCompany);
        group.MapPatch("/{id}/description", ChangeDescription);
        group.MapPatch("/{id}/address", ChangeAddress);
        group.MapDelete("/{id}", DeleteCompany);
    }

    private record RegisterCompanyRequest(string Name, string Description, AddressRequest Address);
    private record AddressRequest(string Street, string City, string State, string Country, string ZipCode);
    private record RenameCompanyRequest(string Name);
    private record ChangeDescriptionRequest(string Description);
    private record ChangeAddressRequest(AddressRequest Address);

    private static async Task<ICompanyQueries.CompanyResponse[]> GetCompanies(ICompanyQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetCompaniesAsync(cancellationToken);

    private static async Task<IResult> GetCompany(Guid id, ICompanyQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetCompanyAsync(id, cancellationToken) is ICompanyQueries.CompanyResponse company
        ? Results.Ok(company)
        : Results.NotFound();

    private static async Task<IResult> RegisterCompany(RegisterCompanyRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = Company.Register(
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

        unitOfWork.GetRepository<Company, Company.CompanyId>().Add(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.Created($"/companies/{company.PublicId}", company.PublicId.Value);
    }

    private static async Task<IResult> RenameCompany(Guid id, RenameCompanyRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = await unitOfWork.GetRepository<Company, Company.CompanyId>().TryFindAsync(id, cancellationToken);

        if (company == null) return Results.NotFound();

        company.Rename(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> ChangeDescription(Guid id, ChangeDescriptionRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = await unitOfWork.GetRepository<Company, Company.CompanyId>().TryFindAsync(id, cancellationToken);

        if (company == null) return Results.NotFound();

        company.ChangeDescription(request.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> ChangeAddress(Guid id, ChangeAddressRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = await unitOfWork.GetRepository<Company, Company.CompanyId>().TryFindAsync(id, cancellationToken);

        if (company == null) return Results.NotFound();

        company.ChangeAddress(new(
            request.Address.Street,
            request.Address.City,
            request.Address.State,
            request.Address.Country,
            request.Address.ZipCode
        ));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteCompany(Guid id, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        await unitOfWork.GetRepository<Company, Company.CompanyId>().Delete(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}