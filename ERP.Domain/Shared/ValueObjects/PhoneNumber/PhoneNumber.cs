using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record PhoneNumber
{
    public string Value { get; init; }

    public PhoneNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new DomainException(PhoneNumberErrors.InputEmpty);
        if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^\+?([0-9\-\s\(\)\/]){6,15}[0-9]$")) throw new DomainException(PhoneNumberErrors.InputInvalidFormat);

        Value = Normalize(input);
    }

    private static string Normalize(string input)
    {
        var digits = new string(input.Where(char.IsDigit).ToArray());

        if (input.StartsWith('+')) digits = "+" + digits;

        return digits;
    }
}