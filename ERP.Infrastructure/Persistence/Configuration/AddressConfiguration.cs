using ERP.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.Persistence.Configuration;

public static class AddressMappingExtension
{
    public static void ConfigureAddress(this ComplexPropertyBuilder<Address> builder)
    {
        builder.Property(_ => _.Street).HasMaxLength(255);
        builder.Property(_ => _.City).HasMaxLength(100);
        builder.Property(_ => _.State).HasMaxLength(100);
        builder.Property(_ => _.Country).HasMaxLength(100);
        builder.Property(_ => _.ZipCode).HasMaxLength(20);
    }
}