using BookingOffline.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingOffline.Repositories
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<AlipayUser> AlipayUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemOption> OrderItemOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
            //.UseLazyLoadingProxies()
            .UseSqlite("Data Source=sqlitedemo.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(s => s.OrderItems)
                .WithOne(s => s.Order)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);
            modelBuilder.Entity<OrderItem>()
                .HasMany(s => s.OrderItemOptions)
                .WithOne(s => s.OrderItem)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();
        }
    }
}
