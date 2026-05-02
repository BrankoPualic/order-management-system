namespace ERP.Infrastructure.Queries;

public interface ICompanyQueries
{
    public record CompanyResponse(Guid Id, string Name, string Description, DateTime CreatedOn);

    Task<CompanyResponse[]> GetCompaniesAsync(CancellationToken cancellationToken = default);
}