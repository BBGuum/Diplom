using Microsoft.EntityFrameworkCore;
using TrueDiplom.Models;

namespace TrueDiplom.Utilities
{
    class DbAppContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=Diplom");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(e => e.RoleEntity)
                .WithMany(e => e.UserEntities);
            modelBuilder.Entity<Product>()
                .HasOne(e => e.CategoryEntity)
                .WithMany(e => e.ProductEntities);
            modelBuilder.Entity<Order>()
                .HasOne(e => e.StatusEntity)
                .WithMany(e => e.OrderEntites)
                .HasForeignKey(e => e.StatusId);
            modelBuilder.Entity<Product>()
                .HasMany(e => e.UserEntities)
                .WithMany(e => e.ProductEntities)
                .UsingEntity<Cart>(
                    a => a.HasOne(a => a.UserEntity).WithMany(c => c.CartEntities).HasForeignKey(c => c.UserId),
                    b => b.HasOne(b => b.ProductEntity).WithMany(c => c.CartEntities).HasForeignKey(c => c.ProductId));
            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderEntities)
                .WithMany(e => e.ProductEntities)
                .UsingEntity<OrderProduct>(
                    a => a.HasOne(a => a.OrderEntity).WithMany(c => c.OrderProducts).HasForeignKey(c => c.OrderId),
                    b => b.HasOne(b => b.ProductEntity).WithMany(c => c.OrderProducts).HasForeignKey(c => c.ProductId));
        }
    }
}
