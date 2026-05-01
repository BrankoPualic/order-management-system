using ERP.Domain;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Persistence;

public partial class ApplicationDbContext : ICompanyRepository
{
    public DbSet<Company> Companies => Set<Company>();

    public async Task<Company?> TryFindAsync(Company.CompanyId key) => await Companies.FirstOrDefaultAsync(_ => _.PublicId == key);
    public void Add(Company aggregate) => Companies.Add(aggregate);
    public void Delete(Company aggregate) => Companies.Remove(aggregate);
}