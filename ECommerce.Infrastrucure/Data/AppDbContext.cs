using ECommerce.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           

            //modelBuilder.Entity<Product>().HasData(
            //        new Product { Id = 1, Name = "Product 1", Description = "Product 1 description", Price = 20, CategoryId = 3 },
            //        new Product { Id = 2, Name = "Product 2", Description = "Product 2 description", Price = 50, CategoryId = 4 }
            //    );
        }

    }
}
