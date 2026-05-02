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
            company.CreatedOn,
            new ICompanyQueries.AddressResponse(
                company.Address.Street,
                company.Address.City,
                company.Address.State,
                company.Address.Country,
                company.Address.ZipCode
            )
        ))
        .AsNoTracking()
        .ToArrayAsync(cancellationToken);

    public async Task<ICompanyQueries.CompanyResponse?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default) =>
        await dbContext.Companies
        .Where(company => company.PublicId == id)
        .Select(company => new ICompanyQueries.CompanyResponse(
            company.PublicId.Value,
            company.Name,
            company.Description,
            company.CreatedOn,
            new ICompanyQueries.AddressResponse(
                company.Address.Street,
                company.Address.City,
                company.Address.State,
                company.Address.Country,
                company.Address.ZipCode
            )
        ))
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
}