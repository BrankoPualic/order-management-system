using ERP.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Test.Integration.Base;

public abstract class IntegrationTestBase : IntegrationTestFixture
{
    private IServiceScope _scope = null!;

    [SetUp]
    public void SetUp()
    {
        _scope = ServiceProvider.CreateScope();
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    private IServiceProvider Services => _scope.ServiceProvider;

    protected T Get<T>() where T : notnull => Services.GetRequiredService<T>();

    protected IUnitOfWork UnitOfWork => Get<IUnitOfWork>();
}
//     protected IntegrationTestFixture Fixture { get; }

//     protected IntegrationTestBase()
//     {
//         Fixture = new IntegrationTestFixture();
//     }
// }