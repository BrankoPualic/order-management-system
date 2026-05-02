using ERP.Domain;
using ERP.Domain.Shared;
using ERP.Domain.Shared.ValueObjects;

namespace ERP.API.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/companies");

        group.MapPost("/", RegisterCompany);
    }

    private static async Task<IResult> RegisterCompany(RegisterCompanyRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var company = Company.Register(request.Name, request.Description, request.Address.CreateAddress());

        unitOfWork.GetRepository<Company, Company.CompanyId>().Add(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Results.Created($"/companies/{company.PublicId}", company);
    }

    private record RegisterCompanyRequest(string Name, string Description, AddressRequest Address);
    private record AddressRequest(string Street, string City, string State, string Country, string ZipCode)
    {
        public Address CreateAddress() => new(Street, City, State, Country, ZipCode);
    }
}