using System.Linq;
using BookingOffline.Entities;

namespace BookingOffline.Repositories
{
    public interface IOrderRepository : IRepository<Order, string>
    {
    }
}