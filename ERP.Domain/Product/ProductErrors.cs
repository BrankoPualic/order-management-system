using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain;

public static class ProductErrors
{
    public static readonly DomainError NameEmpty = new("product.name.empty", "Product name cannot be empty");
    public static readonly DomainError NameTooLong = new("product.name.too.long", "Product name too long");

    public static readonly DomainError DescriptionEmpty = new("product.description.empty", "Product description cannot be empty");

    public static readonly DomainError PriceEmpty = new("product.price.empty", "Product price cannot be empty");
}