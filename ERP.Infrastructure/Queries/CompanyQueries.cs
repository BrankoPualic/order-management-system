using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Queries;

public class CompanyQueries(ApplicationDbContext dbContext) : ICompanyQueries
{
    public async Task<ICompanyQueries.CompanyResponse[]> GetCompaniesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Companies
        .Select(company => new ICompanyQueries.CompanyResponse(
            company.PublicId.Value,
            company.Name,
            company.Description,
            company.CreatedOn
        ))
        .AsNoTracking()
        .ToArrayAsync(cancellationToken);
}