using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain.Shared.ValueObjects;

public static class CurrencyErrors
{
    public static readonly DomainError CodeEmpty = new("currency.code.empty", "Currency code cannot be empty");
    public static readonly DomainError CodeFormat = new("currency.code.format", "Currency code invalid format");
}