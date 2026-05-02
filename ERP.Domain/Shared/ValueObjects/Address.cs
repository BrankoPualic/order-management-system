namespace ERP.Domain.Shared.ValueObjects;

public sealed record Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    public Address(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required");
        if (street.Length > 255) throw new ArgumentException("Street is longer than 255 characters");

        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
        if (city.Length > 100) throw new ArgumentException("City is longer than 100 characters");

        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required");
        if (state.Length > 100) throw new ArgumentException("State is longer than 100 characters");

        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");
        if (country.Length > 100) throw new ArgumentException("Country is longer than 100 characters");

        if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("ZipCode is required");
        if (zipCode.Length > 20) throw new ArgumentException("ZipCode is longer than 20 characters");

        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
}