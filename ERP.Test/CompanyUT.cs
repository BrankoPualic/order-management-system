using ERP.Domain;
using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test;

[TestFixture]
public class CompanyUT
{
    private Address _address;
    private static string _validLongName;
    private string _invalidLongName;

    [SetUp]
    public void SetUp()
    {
        _address = new("a", "a", "a", "a", "a");
        _validLongName = new string('a', 255);
        _invalidLongName = new string('a', 256);
    }

    [Test]
    public void Should_Register_Company_When_Data_Is_Valid()
    {
        Company company = null!;
        Action act = () => company = Company.Register(_validLongName, "a", _address);
        act.Should().NotThrow();
        company?.Name.Should().Be(_validLongName);
    }

    [TestCase("", "description")]
    [TestCase(" ", "description")]
    [TestCase(null, "description")]

    [TestCase("name", "")]
    [TestCase("name", " ")]
    [TestCase("name", null)]
    public void Should_Throw_When_Name_Or_Description_Is_Invalid(string? name, string? description)
    {
        Action act = () => Company.Register(name!, description!, _address);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_When_Name_Is_Too_Long()
    {
        Action act = () => Company.Register(_invalidLongName, "a", _address);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Rename_When_Name_Is_Valid()
    {
        Action act = () => Company.Register("a", "2", _address);
        act.Should().NotThrow<ArgumentException>();
    }

    [Test]
    public void Should_Rename_When_Name_Is_Long_Enough()
    {
        Action act = () => Company.Register(_validLongName, "2", _address);
        act.Should().NotThrow<ArgumentException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null!)]
    public void Should_Throw_When_Rename_Is_Invalid(string name)
    {
        Company company = Company.Register(_validLongName, "a", _address);
        Action act = () => company.Rename(name);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_When_Rename_Is_Too_Long()
    {
        Company company = Company.Register(_validLongName, "a", _address);
        Action act = () => company.Rename(_invalidLongName);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_When_Trying_To_Set_Invalid_Address()
    {
        Company company = Company.Register("My", "a", _address);
        Action act = () => company.ChangeAddress(null!);
        act.Should().Throw<ArgumentNullException>(because: "Address is null");
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null!)]
    public void Should_Throw_When_UpdateDescription_Is_Invalid(string description)
    {
        Company company = Company.Register(_validLongName, "a", _address);
        Action act = () => company.ChangeDescription(description);
        act.Should().Throw<ArgumentException>();
    }
}