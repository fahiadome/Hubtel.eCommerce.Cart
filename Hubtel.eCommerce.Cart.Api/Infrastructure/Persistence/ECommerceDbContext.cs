using System;
using System.Collections.Generic;
using System.Data.Entity;
using Hubtel.eCommerce.Cart.Api.Core.Domain.Enums;
using Hubtel.eCommerce.Cart.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence
{
    public class ECommerceDbContext: IdentityDbContext<User>  
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Item> Items { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ItemVendor> ItemVendors { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ItemCategory> ItemCategories  { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Address> Addresses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users  { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<IdentityRole> Roles { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Cart> Carts  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,4)");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cart>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<ItemVendor>()
                .HasData(
                    new ItemVendor
                    {
                        Id = new Guid("F2562115-0B80-479B-94FB-71D03319D574"),
                        Name = "Vendor one",
                        Code = "IT101",
                        Description = "Some description"
                    },
                    new ItemVendor
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vendor two",
                        Code = "IT102",
                        Description = "Some other description"
                    }
                );
            modelBuilder.Entity<ItemCategory>()
                .HasData(
                    new ItemCategory
                    {
                        Id = new Guid("F2562115-0B80-479B-94FB-71D03319D574"),
                        Name = "Cat one",
                        Code = "CAT101",
                        Description = "Some description"
                    },
                    new ItemCategory
                    {
                        Id = new Guid("27C3CCD2-90BF-4A36-A424-B0D47FEB257F"),
                        Name = "Cat two",
                        Code = "CAT102",
                        Description = "Some other description"
                    }
                );

            modelBuilder.Entity<Item>()
                .HasData(
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        ItemName = "Item one",
                        Quantity = 30,
                        UnitPrice = 12,
                        SKU = "sku101",
                        Status = ItemStatus.InStock,
                        VendorId = new Guid("F2562115-0B80-479B-94FB-71D03319D574"),
                        ItemCategoryId = new Guid("27C3CCD2-90BF-4A36-A424-B0D47FEB257F"),
                        Description = ""
                    }

                );

            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer
                    {
                        Id = new Guid("9DE9254D-C3DC-4AF2-810C-E924682DC173"),
                        FirstName = "Franklin",
                        LastName = "Kwofie",
                        OtherName = "",
                        UserId = new Guid("d67e5380-a832-4f3d-ad42-5cba564f7ad8")
                    }
                );

            modelBuilder.Entity<Address>()
                .HasData(
                    new Address
                    {
                        Id = Guid.NewGuid(),
                        PhoneNumber = "0244444444",
                        Country = "Ghana",
                        State ="Western",
                        City = "Tarkwa",
                        Email ="abc@abc.com",
                        ZipCode = "233",
                        AddressType = AddressType.Primary,
                        AddressLine1 = "P.O. Box 22",
                        AddressLine2 ="Lowcost",
                        AddressLine3 = "",
                        AddressLine4 = "",
                        CustomerId = new Guid("9DE9254D-C3DC-4AF2-810C-E924682DC173")
                    }
                );

        }

    }

}
