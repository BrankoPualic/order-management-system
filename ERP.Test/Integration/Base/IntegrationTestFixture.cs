using ERP.Domain.Shared;
using ERP.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Test.Integration.Base;

public class IntegrationTestFixture
{
    public ServiceProvider ServiceProvider { get; private set; } = null!;
    public SqliteConnection Connection { get; private set; } = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Connection));

        services.AddScoped<IUnitOfWork, ApplicationDbContext>();

        ServiceProvider = services.BuildServiceProvider();

        using var scope = ServiceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ServiceProvider.Dispose();
        Connection.Dispose();
    }
}