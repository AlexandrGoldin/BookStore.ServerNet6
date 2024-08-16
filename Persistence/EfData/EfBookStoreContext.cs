using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistence.EfData
{
    public class EfBookStoreContext : DbContext
    {
        public DbSet<Product>? Products { get; set; } 
        public DbSet<CartItem>? CartItems { get; set; } 
        public DbSet<Order>? Orders { get; set; }

        // This constructor need for repository testing
        public EfBookStoreContext() { }

        public EfBookStoreContext(DbContextOptions<EfBookStoreContext> options)
           : base(options)  
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(SeedData.Products);
        }
    }
}


