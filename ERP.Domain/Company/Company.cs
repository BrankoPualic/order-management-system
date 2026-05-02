using ERP.Domain.Shared.Base;
using ERP.Domain.Shared.ValueObjects;

namespace ERP.Domain;

public class Company : IAggregateRoot
{
    public readonly record struct CompanyId(Guid Value)
    {
        public static CompanyId Empty { get; } = new(Guid.Empty);
        public static CompanyId NewId() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();
        public static implicit operator CompanyId(Guid value) => new(value);
    }

    public CompanyId PublicId { get; private set; } = CompanyId.NewId();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Address Address { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; }

    private Company() { }
    private Company(string name, string description, Address address) : this()
    {
        Name = name;
        Description = description;
        Address = address ?? throw new ArgumentNullException("Address is required");
        CreatedOn = DateTime.UtcNow;
    }

    public static Company Register(string name, string description, Address address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
        if (name.Length > 255) throw new ArgumentException("Name is longer than 255 characters");

        return new(name, description, address);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (name.Length > 255) throw new ArgumentException("Name is longer than 255 characters");
        Name = name;
    }

    public void ChangeAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException("Address is required");
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
        Description = description;
    }
}