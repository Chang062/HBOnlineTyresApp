using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options): base(options)
        {


        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Tyre> Tyres { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }

        //orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet <ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

