using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Quantity)
            .IsRequired();

        builder.Property(s => s.ProductId)
            .IsRequired();

        builder.Property(s => s.UnitPrice)
            .IsRequired();

        builder.Property(s => s.Discount)
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .IsRequired();

        builder.Property(s => s.SaleId)
            .IsRequired();
    }
}

