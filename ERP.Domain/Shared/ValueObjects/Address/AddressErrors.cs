using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public static class AddressErrors
{
    public static readonly DomainError StreetEmpty = new("address.street.empty", "Address street cannot be empty");
    public static readonly DomainError StreetTooLong = new("address.street.too.long", "Address street too long");

    public static readonly DomainError CityEmpty = new("address.city.empty", "Address city cannot be empty");
    public static readonly DomainError CityTooLong = new("address.city.too.long", "Address city too long");

    public static readonly DomainError StateEmpty = new("address.state.empty", "Address state cannot be empty");
    public static readonly DomainError StateTooLong = new("address.state.too.long", "Address state too long");

    public static readonly DomainError CountryEmpty = new("address.country.empty", "Address country cannot be empty");
    public static readonly DomainError CountryTooLong = new("address.country.too.long", "Address country too long");

    public static readonly DomainError ZipCodeEmpty = new("address.zipcode.empty", "Address zip code cannot be empty");
    public static readonly DomainError ZipCodeTooLong = new("address.zipcode.too.long", "Address zip code too long");
}