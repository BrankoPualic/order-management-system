using ERP.Domain;
using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class CompanyUT
{
    private static Address _address;

    [SetUp]
    public void SetUp()
    {
        _address = new("street", "city", "state", "country", "zip");
    }

    [Test]
    public void Should_RegisterCompany_When_CompanyIsValid()
    {
        Company company = Company.Register("name", "description", _address);
        company.Name.Should().Be("name");
        company.Description.Should().Be("description");
        company.Address.Should().Be(_address);
        company.PublicId.Should().NotBeNull();
        company.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
    }

    private static readonly object?[] CompanyInvalidCases = [
        new object?[] { "", "description", _address },
        new object?[] { " ", "description", _address },
        new object?[] { null, "description", _address },
        new object?[] { new string('a', 256), "description", _address },

        new object?[] { "name", "", _address },
        new object?[] { "name", " ", _address },
        new object?[] { "name", null, _address },

        new object?[] { "name", "description", null },
    ];
    [TestCaseSource(nameof(CompanyInvalidCases))]
    public void Should_Throw_When_CompanyIsInvalid(string? name, string? description, Address? address)
    {
        Action act = () => Company.Register(name!, description!, address!);
        act.Should().Throw();
    }

    [Test]
    public void Should_Rename_When_NameIsValid()
    {
        Company company = Company.Register("name", "description", _address);
        company.Rename("new name");
        company.Name.Should().Be("new name");
    }

    private readonly static string?[] InvalidRenamingCase = ["", " ", null, new string('a', 256)];
    [TestCaseSource(nameof(InvalidRenamingCase))]
    public void Should_Throw_When_InvalidRenaming(string? name)
    {
        Company company = Company.Register("name", "description", _address);
        Action act = () => company.Rename(name!);
        act.Should().Throw();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Should_Throw_When_SettingInvalidDescription(string? description)
    {
        Company company = Company.Register("name", "description", _address);
        Action act = () => company.ChangeDescription(description!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangeDescription_When_DescriptionIsValid()
    {
        Company company = Company.Register("name", "description", _address);
        company.ChangeDescription("new description");
        company.Description.Should().Be("new description");
    }

    [Test]
    public void Should_Throw_When_SettingInvalidAddress()
    {
        Company company = Company.Register("name", "description", _address);
        Action act = () => company.ChangeAddress(null!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangeAddress_When_AddressIsValid()
    {
        Company company = Company.Register("name", "description", _address);
        company.ChangeAddress(new Address("new street", "new city", "new state", "new country", "new zip"));
        company.Address.Street.Should().Be("new street");
        company.Address.City.Should().Be("new city");
        company.Address.State.Should().Be("new state");
        company.Address.Country.Should().Be("new country");
        company.Address.ZipCode.Should().Be("new zip");
    }
}