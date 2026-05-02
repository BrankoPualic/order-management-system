namespace ERP.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency)
    {
        if (amount <= 0) throw new ArgumentException("Amount is required");

        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required");
        if (currency.Length != 3) throw new ArgumentException("Currency must be ISO code format");

        Amount = amount;
        Currency = currency;
    }
}