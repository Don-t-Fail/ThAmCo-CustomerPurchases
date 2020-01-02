using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPurchases.Data.Purchases;
using CustomerPurchases.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CustomerPurchases.Data
{
    public class PurchaseDbContext : DbContext
    {
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }

        private IHostingEnvironment HostEnv { get; }

        public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options, IHostingEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                //SeedData
                builder.Entity<Product>().HasData(
                    new Product { Id = 1, Name = "Hydrogen", StockLevel = 1 },
                    new Product { Id = 2, Name = "Helium", StockLevel = 0 },
                    new Product { Id = 3, Name = "Lithium", StockLevel = 300 }
                );

                builder.Entity<Purchase>().HasData(
                    new Purchase { Id = 1, AccountId = 1, ProductId = 1, AddressId = 1, OrderStatus = OrderStatus.Created, Qty = 1, TimeStamp = DateTime.Now},
                    new Purchase { Id = 2, ProductId = 1, AccountId = 1, AddressId = 3, OrderStatus = OrderStatus.Completed, Qty = 4, TimeStamp = DateTime.Now },
                    new Purchase { Id = 3, AddressId = 2, AccountId = 3, OrderStatus = OrderStatus.Shipped, ProductId = 3, Qty = 2, TimeStamp = DateTime.Now }
                );
            }
        }
    }
}
