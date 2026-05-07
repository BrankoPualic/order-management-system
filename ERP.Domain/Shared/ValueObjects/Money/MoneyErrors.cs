using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public static class MoneyErrors
{
    public static readonly DomainError AmountEmpty = new("money.amount.empty", "Money amount cannot be empty");
    public static readonly DomainError AddMoneyInvalid = new("money.add.invalid", "Cannot add money of different currencies");
    public static readonly DomainError SubtractMoneyInvalid = new("money.subtract.invalid", "Cannot subtract money of different currencies");
    public static readonly DomainError SubtractMoneyNegative = new("money.subtract.negative", "Subtract cannot result in negative value");
    public static readonly DomainError ScaleMoneyNegative = new("money.scale.negative", "Cannot scale with negative value");
    public static readonly DomainError CurrencyEmpty = new("money.currency.empty", "Money currency cannot be empty");
}