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
            => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}
