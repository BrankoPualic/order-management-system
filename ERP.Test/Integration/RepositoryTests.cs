using ERP.Domain;
using ERP.Domain.Shared.ValueObjects;
using ERP.Test.Integration.Base;
using FluentAssertions;

namespace ERP.Test.Integration;

[TestFixture]
public class RepositoryTests : IntegrationTestBase
{

    private readonly Company _company;

    public RepositoryTests() : base()
    {
        _company = Company.Register("name", "description", Address.Create("street", "city", "state", "country", "zip"));
    }

    [Test]
    public async Task Should_ReturnAggregate_When_RequestedById()
    {
        var repository = UnitOfWork.GetRepository<Company, Company.CompanyId>();
        repository.Add(_company);
        await UnitOfWork.SaveChangesAsync();

        var result = await repository.TryFindAsync(_company.PublicId);
        result.Should().NotBeNull();
    }

    [Test]
    public async Task Should_DeleteAggregate_When_Found()
    {
        var repository = UnitOfWork.GetRepository<Company, Company.CompanyId>();
        repository.Add(_company);
        await UnitOfWork.SaveChangesAsync();

        repository.Delete(_company);
        await UnitOfWork.SaveChangesAsync();

        var result = await repository.TryFindAsync(_company.PublicId);
        result.Should().BeNull();
    }

    [Test]
    public async Task Should_DeleteAggregateByKey_When_Found()
    {
        var repository = UnitOfWork.GetRepository<Company, Company.CompanyId>();
        repository.Add(_company);
        await UnitOfWork.SaveChangesAsync();

        await repository.Delete(_company.PublicId);
        await UnitOfWork.SaveChangesAsync();

        var result = await repository.TryFindAsync(_company.PublicId);
        result.Should().BeNull();
    }

    [Test]
    public async Task Should_ThrowDeleteByKey_When_NotFound()
    {
        var repository = UnitOfWork.GetRepository<Company, Company.CompanyId>();
        var act = () => repository.Delete(Company.CompanyId.New());
        await act.Should().ThrowAsync();
    }
}