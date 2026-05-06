using ERP.Domain;
using ERP.Infrastructure.Queries.Responses;

namespace ERP.Infrastructure.Queries;

public interface ICompanyQueries
{
    public record CompanyResponse(Guid Id, string Name, string Description, DateTime CreatedOn, AddressResponse Address);

    Task<CompanyResponse[]> GetCompaniesAsync(CancellationToken cancellationToken = default);
    Task<CompanyResponse?> GetCompanyAsync(Company.CompanyId id, CancellationToken cancellationToken = default);
}