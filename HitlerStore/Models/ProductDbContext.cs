using Microsoft.EntityFrameworkCore;

namespace HitlerStore.Models
{
    public class ProductDbContext : DbContext
    {
         public ProductDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> ProductsTable { get; set; }
    }
}
