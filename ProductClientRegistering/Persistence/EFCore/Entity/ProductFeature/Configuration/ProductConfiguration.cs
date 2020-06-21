using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EFCore.Entity.ProductFeature.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id);

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,4)");

        }
    }
}
