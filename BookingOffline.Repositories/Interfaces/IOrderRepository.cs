using System.Linq;
using BookingOffline.Entities;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order, string>
    {
    }
}