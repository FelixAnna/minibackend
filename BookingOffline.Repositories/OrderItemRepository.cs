using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
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
            return _context.OrderItems.FirstOrDefault(x => x.OrderItemId == key);
        }

        public OrderItem Create(OrderItem item)
        {
            var newItem = _context.OrderItems.Add(item);
            _context.SaveChanges();
            return newItem.Entity;
        }

        public bool Delete(int key, string userId)
        {
            var order = _context.OrderItems.FirstOrDefault(x => x.OrderItemId == key && x.CreatedBy == userId);
            if (order == null)
            {
                return false;
            }

            _context.OrderItems.Remove(order);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<OrderItem> FindAll(string orderId)
        {
            return _context.OrderItems.Where(x => x.OrderId == orderId);
        }
    }
}
