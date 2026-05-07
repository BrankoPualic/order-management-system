using ERP.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.Persistence.Configuration;

public static class MoneyMappingExtension
{
    public static void ConfigureMoney(this ComplexPropertyBuilder<Money> builder)
    {
        builder.ComplexProperty(_ => _.Currency).ConfigureCurrency();
    }
}