using ERP.Domain;
using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class WarehouseUT
{
    private static Address _address;

    [SetUp]
    public void SetUp()
    {
        _address = Address.Create("street", "city", "state", "country", "zip");
    }

    [Test]
    public void Should_RegisterWarehouse_When_WarehouseIsValid()
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        warehouse.Name.Should().Be("name");
        warehouse.Description.Should().Be("description");
        warehouse.Address.Should().Be(_address);
        warehouse.PublicId.Should().NotBeNull();
        warehouse.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
    }

    private static readonly object?[] WarehouseInvalidCases = [
        new object?[] { "", "description", _address },
        new object?[] { " ", "description", _address },
        new object?[] { null, "description", _address },
        new object?[] { new string('a', 256), "description", _address },

        new object?[] { "name", "", _address },
        new object?[] { "name", " ", _address },
        new object?[] { "name", null, _address },

        new object?[] { "name", "description", null },
    ];
    [TestCaseSource(nameof(WarehouseInvalidCases))]
    public void Should_Throw_When_WarehouseIsInvalid(string? name, string? description, Address? address)
    {
        Action act = () => Warehouse.Register(name!, description!, address!);
        act.Should().Throw();
    }

    [Test]
    public void Should_Rename_When_NameIsValid()
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        warehouse.Rename("new name");
        warehouse.Name.Should().Be("new name");
    }

    private readonly static string?[] InvalidRenamingCase = ["", " ", null, new string('a', 256)];
    [TestCaseSource(nameof(InvalidRenamingCase))]
    public void Should_Throw_When_InvalidRenaming(string? name)
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        Action act = () => warehouse.Rename(name!);
        act.Should().Throw();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Should_Throw_When_SettingInvalidDescription(string? description)
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        Action act = () => warehouse.ChangeDescription(description!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangeDescription_When_DescriptionIsValid()
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        warehouse.ChangeDescription("new description");
        warehouse.Description.Should().Be("new description");
    }

    [Test]
    public void Should_Throw_When_SettingInvalidAddress()
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        Action act = () => warehouse.ChangeAddress(null!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangeAddress_When_AddressIsValid()
    {
        Warehouse warehouse = Warehouse.Register("name", "description", _address);
        warehouse.ChangeAddress(Address.Create("new street", "new city", "new state", "new country", "new zip"));
        warehouse.Address.Street.Should().Be("new street");
        warehouse.Address.City.Should().Be("new city");
        warehouse.Address.State.Should().Be("new state");
        warehouse.Address.Country.Should().Be("new country");
        warehouse.Address.ZipCode.Should().Be("new zip");
    }
}