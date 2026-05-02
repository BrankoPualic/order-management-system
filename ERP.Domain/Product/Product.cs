using ERP.Domain.Shared.Base;
using ERP.Domain.Shared.ValueObjects;

namespace ERP.Domain;

public class Product : IAggregateRoot
{
    public readonly record struct ProductId(Guid Value)
    {
        public static ProductId Empty { get; } = new(Guid.Empty);
        public static ProductId NewId() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();
        public static implicit operator ProductId(Guid value) => new(value);
    }

    public ProductId PublicId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; }

    // Needed for EF Core
    private Product() { }
    private Product(string name, string description, Money price) : this()
    {
        Name = name;
        Description = description;
        Price = price ?? throw new ArgumentNullException("Price is required");
        CreatedOn = DateTime.UtcNow;
    }

    public static Product Register(string name, string description, Money price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
        if (name.Length > 255) throw new ArgumentException("Name is longer than 255 characters");

        return new(name, description, price);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (name.Length > 255) throw new ArgumentException("Name is longer than 255 characters");
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
        Description = description;
    }

    public void ChangePrice(Money price)
    {
        Price = price ?? throw new ArgumentNullException("Price is required");
    }
}