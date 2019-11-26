using System.Linq;
using BookingOffline.Entities;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem, int>
    {
        IQueryable<OrderItem> FindAll(string orderId);
    }
}