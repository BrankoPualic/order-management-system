using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test;

[TestFixture]
public class AddressUT
{
    [Test]
    public void Should_Create_Address_When_Data_Is_Valid()
    {
        Address address = null!;
        Action act = () => address = new Address("street", "city", "state", "country", "zip");
        act.Should().NotThrow();
        address?.Street.Should().Be("street");
    }

    [TestCase("", "city", "state", "country", "zip")]
    [TestCase(" ", "city", "state", "country", "zip")]
    [TestCase(null, "city", "state", "country", "zip")]

    [TestCase("street", "", "state", "country", "zip")]
    [TestCase("street", " ", "state", "country", "zip")]
    [TestCase("street", null, "state", "country", "zip")]

    [TestCase("street", "city", "", "country", "zip")]
    [TestCase("street", "city", " ", "country", "zip")]
    [TestCase("street", "city", null, "country", "zip")]

    [TestCase("street", "city", "state", "", "zip")]
    [TestCase("street", "city", "state", " ", "zip")]
    [TestCase("street", "city", "state", null, "zip")]

    [TestCase("street", "city", "state", "country", "")]
    [TestCase("street", "city", "state", "country", " ")]
    [TestCase("street", "city", "state", "country", null)]
    public void Should_Throw_When_Any_Field_Is_Invalid(string? street, string? city, string? state, string? country, string? zip)
    {
        Action act = () => _ = new Address(street!, city!, state!, country!, zip!);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_For_Long_Street()
    {
        var street = new string('a', 256);
        Action act = () => _ = new Address(street, "city", "state", "country", "zip");
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_For_Long_City()
    {
        var city = new string('a', 101);
        Action act = () => _ = new Address("street", city, "state", "country", "zip");
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_For_Long_State()
    {
        var state = new string('a', 101);
        Action act = () => _ = new Address("street", "city", state, "country", "zip");
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_For_Long_Country()
    {
        var country = new string('a', 101);
        Action act = () => _ = new Address("street", "city", "state", country, "zip");
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Should_Throw_For_Long_ZipCode()
    {
        var zip = new string('a', 21);
        Action act = () => _ = new Address("street", "city", "state", "country", zip);
        act.Should().Throw<ArgumentException>();
    }
}