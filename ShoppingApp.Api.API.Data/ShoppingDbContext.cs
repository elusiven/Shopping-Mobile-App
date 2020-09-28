using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Api.API.Data.Entities;

namespace ShoppingApp.Api.API.Data
{
    public class ShoppingDbContext : IdentityDbContext<UserEntity>
    {
        public ShoppingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ShoppingCartItemEntity> ShoppingCartItems { get; set; }
    }
}