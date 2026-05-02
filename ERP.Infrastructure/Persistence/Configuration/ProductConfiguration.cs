using ERP.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERP.Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property(_ => _.PublicId).HasConversion(new ProductIdConverter());
        builder.HasAlternateKey(_ => _.PublicId);

        builder.ComplexProperty(_ => _.Price).ConfigureMoney();

        builder.Property(_ => _.Name).HasMaxLength(255);
    }

    public class ProductIdConverter() : ValueConverter<Product.ProductId, Guid>(id => id.Value, value => new(value));
}