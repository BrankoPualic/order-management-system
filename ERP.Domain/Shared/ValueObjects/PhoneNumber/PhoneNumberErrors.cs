using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public static class PhoneNumberErrors
{
    public static readonly DomainError InputEmpty = new("phonenumber.input.empty", "Phone number cannot be empty");
    public static readonly DomainError InputInvalidFormat = new("phonenumber.input.invalid.format", "Phone number in invalid format");
}