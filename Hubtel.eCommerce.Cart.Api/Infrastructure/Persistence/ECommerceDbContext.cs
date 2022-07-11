using System.Collections.Generic;
using System.Data.Entity;
using Hubtel.eCommerce.Cart.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence
{
    public class ECommerceDbContext:DbContext
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
        //public DbSet<IdentityUserClaim> UserClaims  { get; set; }
        //public DbSet<IdentityUserToken> UserTokens  { get; set; }
        //public DbSet<IdentityUserLogin> UserLogins  { get; set; }
        //public DbSet<IdentityRoleClaim> RoleClaims  { get; set; }
        //public DbSet<IdentityUserRole> UserRoles  { get; set; }

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
        }

    }


    //public class InitialiseDb : DropCreateDatabaseAlways<ECommerceDbContext>
    //{
    //    protected override void Seed(ECommerceDbContext context)
    //    {
    //        IList<ItemCategory> itemCategory = new List<ItemCategory>();

    //        itemCategory.Add(new ItemCategory() { Name = "ItemCategory 1", Code = "A101", Description = "First ItemCategory" });
    //        itemCategory.Add(new ItemCategory() { Name = "ItemCategory 2", Code = "A102", Description = "Second ItemCategory" });
    //        itemCategory.Add(new ItemCategory() { Name = "ItemCategory 3", Code = "A103", Description = "Third ItemCategory" });

    //        context.ItemCategories.AddRange(itemCategory);


    //        IList<ItemVendor> itemVendor  = new List<ItemVendor>();

    //        itemVendor.Add(new ItemVendor() { Name = "Item 1", Code = "A101", Description = "First Item" });
    //        itemVendor.Add(new ItemVendor() { Name = "Item 2", Code = "A102", Description = "Second Item" });
    //        itemVendor.Add(new ItemVendor() { Name = "Item 3", Code = "A103", Description = "Third Item" });

    //        context.ItemVendors.AddRange(itemVendor);

    //        base.Seed(context);

    //    }
    //}
}
