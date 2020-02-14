using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.Sqlite
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BODBContext _context;
        public OrderRepository(BODBContext context)
        {
            _context = context;
        }

        public Order FindById(string key)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(a => a.OrderItemOptions)
                .FirstOrDefault(x => x.OrderId == key);
        }

        public Order Create(Order item)
        {
            var newItem = _context.Orders.Add(item);
            _context.SaveChanges();
            return newItem.Entity;
        }

        public bool Delete(string key, string userId)
        {
            var order = _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(a => a.OrderItemOptions)
                    .FirstOrDefault(x => (x.OrderId == key || string.IsNullOrEmpty(key)) && x.CreatedBy == userId);
            if (order == null)
            {
                return false;
            }

            //because of cascade probelm in sqlite, let remove dependency one by one
            foreach (var item in order.OrderItems)
            {
                _context.Remove(item);
            }

            _context.Remove(order);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Order> FindAll(string userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(a => a.OrderItemOptions)
                .Where(x => x.CreatedBy == userId || x.OrderItems.Any(y => y.CreatedBy == userId));
        }

        public async Task LockOrderAsync(Order order)
        {
            order.State = (int)OrderStatus.Locked;
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UnlockOrderAsync(Order order)
        {
            order.State = (int)OrderStatus.New;
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
