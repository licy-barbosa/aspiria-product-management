using Aspiria.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspiria.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique(); //  Nombre único en DB

            modelBuilder.Entity<Product>()
            .HasCheckConstraint("CK_Product_Price", "[Price] >= 1 AND [Price] <= 1000");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2); // total 10 dígitos, 2 decimales
        }
    }
}