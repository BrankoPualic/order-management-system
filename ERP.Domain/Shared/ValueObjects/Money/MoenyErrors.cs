using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public static class MoneyErrors
{
    public static readonly DomainError AmountEmpty = new("money.amount.empty", "Money amount cannot be empty");

    public static readonly DomainError CurrencyEmpty = new("money.currency.empty", "Money currency cannot be empty");
    public static readonly DomainError CurrencyNotISO = new("money.currency.not.iso", "Money currency not ISO code");
}