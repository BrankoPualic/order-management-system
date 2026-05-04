namespace ERP.Domain.Shared.Exceptions;

public class DomainException(DomainError Error) : Exception(Error.Message)
{
    public string Code { get; } = Error.Code;
}

public record DomainError(string Code, string Message);