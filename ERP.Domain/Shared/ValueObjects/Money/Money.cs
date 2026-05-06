using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        if (amount <= 0) throw new DomainException(MoneyErrors.AmountEmpty);

        if (string.IsNullOrWhiteSpace(currency)) throw new DomainException(MoneyErrors.CurrencyEmpty);
        if (!System.Text.RegularExpressions.Regex.IsMatch(currency, @"^[A-Z]{3}$")) throw new DomainException(MoneyErrors.CurrencyNotISO);

        return new(amount, currency);
    }

    public Money Update(decimal? amount, string? currency) => Create(amount ?? Amount, currency ?? Currency);
}