using BookingOffline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingOffline.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SQLiteDBContext _context;
        public OrderRepository(SQLiteDBContext context)
        {
            _context = context;
        }

        public Order FindById(string key)
        {
            return _context.Orders.FirstOrDefault(x => x.OrderId == key);
        }

        public Order Create(Order item)
        {
            var newItem = _context.Orders.Add(item);
            _context.SaveChanges();
            return newItem.Entity;
        }

        public bool Delete(string key)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == key);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }

        public IQueryable FindAll(string userId)
        {
            return _context.Orders.Where(x => x.CreatedBy == userId);
        }
    }
}
