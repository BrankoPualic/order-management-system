using ERP.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERP.Infrastructure.Persistence.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property(_ => _.PublicId).HasConversion(new CompanyIdConverter());
        builder.HasAlternateKey(_ => _.PublicId);

        builder.ComplexProperty(_ => _.Address).ConfigureAddress();

        builder.Property(_ => _.Name).HasMaxLength(255);
    }

    public class CompanyIdConverter() : ValueConverter<Company.CompanyId, Guid>(id => id.Value, value => new(value));
}