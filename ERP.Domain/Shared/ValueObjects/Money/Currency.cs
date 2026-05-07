using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record Currency
{
    public string Code { get; init; }

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainException(CurrencyErrors.CodeEmpty);
        if (!System.Text.RegularExpressions.Regex.IsMatch(code, @"^[A-Z]{3}$")) throw new DomainException(CurrencyErrors.CodeFormat);

        return new(code);
    }

    public Currency Update(string? code) => new(code ?? Code);
}