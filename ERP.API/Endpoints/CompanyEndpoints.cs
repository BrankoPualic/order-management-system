using ERP.Domain;
using ERP.Domain.Shared;
using ERP.Domain.Shared.ValueObjects;
using ERP.Infrastructure.Queries;

namespace ERP.API.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/companies");

        group.MapGet("/", GetCompanies);
        group.MapGet("/{id}", GetCompany);
        group.MapPost("/", RegisterCompany);
    }

    private record RegisterCompanyRequest(string Name, string Description, AddressRequest Address);
    private record AddressRequest(string Street, string City, string State, string Country, string ZipCode);

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

        return Results.Created($"/companies/{company.PublicId}", company);
    }
}