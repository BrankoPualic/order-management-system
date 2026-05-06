using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    private Address(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Create(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new DomainException(AddressErrors.StreetEmpty);
        if (street.Length > 255) throw new DomainException(AddressErrors.StreetTooLong);

        if (string.IsNullOrWhiteSpace(city)) throw new DomainException(AddressErrors.CityEmpty);
        if (city.Length > 100) throw new DomainException(AddressErrors.CityTooLong);

        if (string.IsNullOrWhiteSpace(state)) throw new DomainException(AddressErrors.StateEmpty);
        if (state.Length > 100) throw new DomainException(AddressErrors.StateTooLong);

        if (string.IsNullOrWhiteSpace(country)) throw new DomainException(AddressErrors.CountryEmpty);
        if (country.Length > 100) throw new DomainException(AddressErrors.CountryTooLong);

        if (string.IsNullOrWhiteSpace(zipCode)) throw new DomainException(AddressErrors.ZipCodeEmpty);
        if (zipCode.Length > 20) throw new DomainException(AddressErrors.ZipCodeTooLong);

        return new(street, city, state, country, zipCode);
    }

    public Address Update(string? street, string? city, string? state, string? country, string? zipCode) => Create(street ?? Street, city ?? City, state ?? State, country ?? Country, zipCode ?? ZipCode);
}