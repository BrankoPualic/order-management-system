using ERP.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.Persistence.Configuration;

public static class CurrencyMappingExtension
{
    public static void ConfigureCurrency(this ComplexPropertyBuilder<Currency> builder)
    {
        builder.Property(_ => _.Code).HasMaxLength(3);
    }
}