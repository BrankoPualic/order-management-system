using ERP.Domain.Shared.Exceptions;

namespace ERP.Domain;

public static class WarehouseErrors
{
    public static readonly DomainError NameEmpty = new("warehouse.name.empty", "Warehouse name cannot be empty");
    public static readonly DomainError NameTooLong = new("warehouse.name.too.long", "Warehouse name too long");

    public static readonly DomainError DescriptionEmpty = new("warehouse.description.empty", "Warehouse description cannot be empty");

    public static readonly DomainError AddressEmpty = new("warehouse.address.empty", "Warehouse address cannot be empty");
}