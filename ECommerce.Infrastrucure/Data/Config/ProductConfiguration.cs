using ECommerce.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.HasData(
                new Product { Id = 1, Name = "Product 1", Description = "Product 1 description", Price = 20, CategoryId = 3 },
                new Product { Id = 2, Name = "Product 2", Description = "Product 2 description", Price = 50, CategoryId = 4 }
            );

        }
    }
}
