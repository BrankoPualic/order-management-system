using ERP.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERP.Infrastructure.Persistence.Configuration;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property(_ => _.PublicId).HasConversion(new WarehouseIdConverter());
        builder.HasAlternateKey(_ => _.PublicId);

        builder.ComplexProperty(_ => _.Address).ConfigureAddress();

        builder.Property(_ => _.Name).HasMaxLength(255);
    }

    public class WarehouseIdConverter() : ValueConverter<Warehouse.WarehouseId, Guid>(id => id.Value, value => new(value));
}