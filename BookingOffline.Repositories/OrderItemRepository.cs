using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingOffline.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SQLiteDBContext _context;
        public OrderItemRepository(SQLiteDBContext context)
        {
            _context = context;
        }

        public OrderItem FindById(int key)
        {
            return _context.OrderItems
                .Include(o => o.OrderItemOptions)
                .FirstOrDefault(x => x.OrderItemId == key);
        }

        public OrderItem Create(OrderItem item)
        {
            var newItem = _context.OrderItems.Add(item);
            _context.SaveChanges();
            return newItem.Entity;
        }

        public bool Delete(int key, string userId)
        {
            var item = _context.OrderItems
                    .Include(a => a.OrderItemOptions)
                    .Include(a => a.Order)
                    .FirstOrDefault(x => x.OrderItemId == key && (x.Order.CreatedBy == userId || x.CreatedBy == userId));
            if (item == null)
            {
                return false;
            }

            _context.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<OrderItem> FindAll(string orderId)
        {
            return _context.OrderItems
                .Include(o => o.OrderItemOptions)
                .Where(x => x.OrderId == orderId);
        }
    }
}
