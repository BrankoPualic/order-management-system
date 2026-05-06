using ERP.Domain.Shared.Base;
using ERP.Domain.Shared.Exceptions;
using ERP.Domain.Shared.ValueObjects;

namespace ERP.Domain;

public class Warehouse : IAggregateRoot
{
    public readonly record struct WarehouseId(Guid Value)
    {
        public static WarehouseId Empty { get; } = new(Guid.Empty);
        public static WarehouseId New() => new(Guid.NewGuid());
    }

    public WarehouseId PublicId { get; private set; } = WarehouseId.New();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Address Address { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; }

    private Warehouse() { }
    private Warehouse(string name, string description, Address address) : this()
    {
        Name = name;
        Description = description;
        Address = address ?? throw new DomainException(WarehouseErrors.AddressEmpty);
        CreatedOn = DateTime.UtcNow;
    }

    public static Warehouse Register(string name, string description, Address address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(WarehouseErrors.NameEmpty);
        if (name.Length > 255) throw new DomainException(WarehouseErrors.NameTooLong);

        if (string.IsNullOrWhiteSpace(description)) throw new DomainException(WarehouseErrors.DescriptionEmpty);

        return new(name, description, address);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(WarehouseErrors.NameEmpty);
        if (name.Length > 255) throw new DomainException(WarehouseErrors.NameTooLong);
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new DomainException(WarehouseErrors.DescriptionEmpty);
        Description = description;
    }

    public void ChangeAddress(Address address)
    {
        Address = address ?? throw new DomainException(WarehouseErrors.AddressEmpty);
    }
}