using CustomerPurchases.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CustomerPurchases.Data
{
    public class PurchaseDbContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<CustomerTel> CustomerTel { get; set; }
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
                builder.Entity<Account>().HasData(
                    new Account { Id = 1, IsStaff = false },
                    new Account { Id = 2, IsStaff = true },
                    new Account { Id = 3, IsStaff = false }
                );

                builder.Entity<CustomerAddress>().HasData(
                    new CustomerAddress { Id = 1, AccountId = 1, Address = "This is an address" },
                    new CustomerAddress { Id = 2, AccountId = 2, Address = "This is an address" },
                    new CustomerAddress { Id = 3, AccountId = 1, Address = "This is also an address" }
                );

                builder.Entity<CustomerTel>().HasData(
                    new CustomerTel { Id = 1, AccountId = 1, TelNo = "This is a number" },
                    new CustomerTel { Id = 2, AccountId = 3, TelNo = "This is a number" },
                    new CustomerTel { Id = 3, AccountId = 1, TelNo = "This is also a number" }
                );

                builder.Entity<Product>().HasData(
                    new Product { Id = 1, Name = "Hydrogen", StockLevel = 1 },
                    new Product { Id = 2, Name = "Helium", StockLevel = 0 },
                    new Product { Id = 3, Name = "Lithium", StockLevel = 300 }
                );

                builder.Entity<Purchase>().HasData(
                    new Purchase { Id = 1, AccountId = 1, ProductId = 1, AddressId = 1, OrderStatus = "Placed", Qty = 1 },
                    new Purchase { Id = 2, ProductId = 1, AccountId = 1, AddressId = 3, OrderStatus = "Complete", Qty = 4 },
                    new Purchase { Id = 3, AddressId = 2, AccountId = 3, OrderStatus = "In Progress", ProductId = 3, Qty = 2 }
                );
            }
        }
    }
}
