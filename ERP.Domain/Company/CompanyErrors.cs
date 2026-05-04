using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain;

public static class CompanyErrors
{
    public static readonly DomainError NameEmpty = new("company.name.empty", "Company name cannot be empty");
    public static readonly DomainError NameTooLong = new("company.name.too.long", "Company name too long");

    public static readonly DomainError DescriptionEmpty = new("company.description.empty", "Company description cannot be empty");

    public static readonly DomainError AddressEmpty = new("company.address.empty", "Address cannot be empty");
}