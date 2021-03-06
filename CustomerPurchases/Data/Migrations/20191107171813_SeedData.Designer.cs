﻿// <auto-generated />
using CustomerPurchases.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerPurchases.Migrations
{
    [DbContext(typeof(PurchaseDbContext))]
    [Migration("20191107171813_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerPurchases.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsStaff");

                    b.HasKey("Id");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 2,
                            IsStaff = true
                        },
                        new
                        {
                            Id = 3,
                            IsStaff = false
                        });
                });

            modelBuilder.Entity("CustomerPurchases.Models.CustomerAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("Address");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("CustomerAddress");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Address = "This is an address"
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            Address = "This is an address"
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 1,
                            Address = "This is also an address"
                        });
                });

            modelBuilder.Entity("CustomerPurchases.Models.CustomerTel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("TelNo");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("CustomerTel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            TelNo = "This is a number"
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 3,
                            TelNo = "This is a number"
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 1,
                            TelNo = "This is also a number"
                        });
                });

            modelBuilder.Entity("CustomerPurchases.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("StockLevel");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Hydrogen",
                            StockLevel = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Helium",
                            StockLevel = 0
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lithium",
                            StockLevel = 300
                        });
                });

            modelBuilder.Entity("CustomerPurchases.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int>("AddressId");

                    b.Property<string>("OrderStatus");

                    b.Property<int>("ProductId");

                    b.Property<int>("Qty");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ProductId");

                    b.ToTable("Purchase");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            AddressId = 1,
                            OrderStatus = "Placed",
                            ProductId = 1,
                            Qty = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 1,
                            AddressId = 3,
                            OrderStatus = "Complete",
                            ProductId = 1,
                            Qty = 4
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 3,
                            AddressId = 2,
                            OrderStatus = "In Progress",
                            ProductId = 3,
                            Qty = 2
                        });
                });

            modelBuilder.Entity("CustomerPurchases.Models.CustomerAddress", b =>
                {
                    b.HasOne("CustomerPurchases.Models.Account", "Account")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CustomerPurchases.Models.CustomerTel", b =>
                {
                    b.HasOne("CustomerPurchases.Models.Account", "Account")
                        .WithMany("CustomerTels")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CustomerPurchases.Models.Purchase", b =>
                {
                    b.HasOne("CustomerPurchases.Models.Account")
                        .WithMany("Purchases")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CustomerPurchases.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
