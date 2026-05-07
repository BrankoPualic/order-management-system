using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; } = null!;

    private Money() { }
    private Money(decimal amount, Currency currency)
    {
        Amount = Math.Round(amount, 2);
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount <= 0) throw new DomainException(MoneyErrors.AmountEmpty);
        if (currency == null) throw new DomainException(MoneyErrors.CurrencyEmpty);

        return new(amount, currency);
    }

    public Money Update(decimal? amount, Currency? currency) => Create(amount ?? Amount, currency ?? Currency);

    public Money Add(Money other) =>
        Currency == other.Currency
        ? new(Amount + other.Amount, Currency)
        : throw new DomainException(MoneyErrors.AddMoneyInvalid);

    public Money Subtract(Money other) =>
        Currency == other.Currency && Amount >= other.Amount
        ? new(Amount - other.Amount, Currency)
        : Currency == other.Currency
        ? throw new DomainException(MoneyErrors.SubtractMoneyNegative)
        : throw new DomainException(MoneyErrors.SubtractMoneyInvalid);

    public Money Scale(decimal factor) =>
        factor <= 0
        ? throw new DomainException(MoneyErrors.ScaleMoneyNegative)
        : new(Amount * factor, Currency);

    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    public static Money operator *(Money left, decimal right) => left.Scale(right);
    public static Money operator *(decimal left, Money right) => right.Scale(left);

    public override string ToString() => $"{Amount:0.00} {Currency.Code}";
}