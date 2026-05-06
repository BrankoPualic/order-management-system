using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class AddressUT
{
    [Test]
    public void Should_CreateAddress_When_AddressIsValid()
    {
        Address address = Address.Create("street", "city", "state", "country", "zip");
        address.Street.Should().Be("street");
        address.City.Should().Be("city");
        address.State.Should().Be("state");
        address.Country.Should().Be("country");
        address.ZipCode.Should().Be("zip");
    }

    private static readonly object?[] InvalidAddressCases = [
        new object?[] { "", "city", "state", "country", "zip" },
        new object?[] { " ", "city", "state", "country", "zip" },
        new object?[] { null, "city", "state", "country", "zip" },
        new object?[] { new string('a', 256), "city", "state", "country", "zip" },

        new object?[] { "street", "", "state", "country", "zip" },
        new object?[] { "street", " ", "state", "country", "zip" },
        new object?[] { "street", null, "state", "country", "zip" },
        new object?[] { "street", new string('a', 101), "state", "country", "zip" },

        new object?[] { "street", "city", "", "country", "zip" },
        new object?[] { "street", "city", " ", "country", "zip" },
        new object?[] { "street", "city", null, "country", "zip" },
        new object?[] { "street", "city", new string('a', 101), "country", "zip" },

        new object?[] { "street", "city", "state", "", "zip" },
        new object?[] { "street", "city", "state", " ", "zip" },
        new object?[] { "street", "city", "state", null, "zip" },
        new object?[] { "street", "city", "state", new string('a', 101), "zip" },

        new object?[] { "street", "city", "state", "country", "" },
        new object?[] { "street", "city", "state", "country", " " },
        new object?[] { "street", "city", "state", "country", null },
        new object?[] { "street", "city", "state", "country", new string('a', 21) },
    ];
    [TestCaseSource(nameof(InvalidAddressCases))]
    public void Should_Throw_When_AddressIsInvalid(string? street, string? city, string? state, string? country, string? zip)
    {
        Action act = () => _ = Address.Create(street!, city!, state!, country!, zip!);
        act.Should().Throw();
    }
}