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
        group.MapPatch("/{id}", UpdateCompanyInformation);
        group.MapDelete("/{id}", DeleteCompany);
    }

    private record RegisterCompanyRequest(string Name, string Description, AddressRequest Address);
    private record AddressRequest(string Street, string City, string State, string Country, string ZipCode);
    private record UpdateCompanyInformationRequest(string Name, string Description, AddressRequest Address);

    private static async Task<ICompanyQueries.CompanyResponse[]> GetCompanies(ICompanyQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetCompaniesAsync(cancellationToken);

    private static async Task<IResult> GetCompany(Guid id, ICompanyQueries queries, CancellationToken cancellationToken = default) =>
        await queries.GetCompanyAsync(new Company.CompanyId(id), cancellationToken) is ICompanyQueries.CompanyResponse company
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

    private static async Task<IResult> UpdateCompanyInformation(Guid id, UpdateCompanyInformationRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = await unitOfWork.GetRepository<Company, Company.CompanyId>().TryFindAsync(new Company.CompanyId(id), cancellationToken);

        if (company == null) return Results.NotFound();

        company.Rename(request.Name);
        company.ChangeDescription(request.Description);
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
        await unitOfWork.GetRepository<Company, Company.CompanyId>().Delete(new Company.CompanyId(id), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}