namespace ERP.Infrastructure.Queries;

public interface ICompanyQueries
{
    public record CompanyResponse(Guid Id, string Name, string Description, DateTime CreatedOn, AddressResponse Address);
    public record AddressResponse(string Street, string City, string State, string Country, string ZipCode);

    Task<CompanyResponse[]> GetCompaniesAsync(CancellationToken cancellationToken = default);
    Task<CompanyResponse?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);
}