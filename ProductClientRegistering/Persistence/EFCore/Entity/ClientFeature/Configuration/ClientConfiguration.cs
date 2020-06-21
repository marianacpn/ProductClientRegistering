using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EFCore.Entity.ClientFeature.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id);

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(500)");


            builder
                .Property(e => e.Sobrenome)
                .HasColumnType("varchar(500)");


            builder
                .Property(e => e.Email)
                .HasColumnType("varchar(500)");


            builder
               .HasMany(e => e.Products)
               .WithOne(e => e.Client)
               .HasForeignKey(e => e.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
