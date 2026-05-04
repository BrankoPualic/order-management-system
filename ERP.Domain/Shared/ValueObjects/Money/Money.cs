using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency)
    {
        if (amount <= 0) throw new DomainException(MoneyErrors.AmountEmpty);

        if (string.IsNullOrWhiteSpace(currency)) throw new DomainException(MoneyErrors.CurrencyEmpty);
        if (currency.Length != 3) throw new DomainException(MoneyErrors.CurrencyNotISO);

        Amount = amount;
        Currency = currency;
    }
}