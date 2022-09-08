using DataLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
            
        }

        public DbSet<Product?> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

    }
}
