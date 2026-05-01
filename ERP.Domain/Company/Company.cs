using ERP.Domain.Shared.Base;
using ERP.Domain.Shared.ValueObjects;

namespace ERP.Domain;

public class Company : IAggregateRoot
{
    public readonly record struct CompanyId(Guid Value)
    {
        public static CompanyId Empty { get; } = new(Guid.Empty);
        public static CompanyId NewId() => new(Guid.NewGuid());
    }

    public CompanyId PublicId { get; private set; } = CompanyId.NewId();
    public string Name { get; private set; } = string.Empty;
    public string Notes { get; private set; } = string.Empty;
    public Address Address { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
}