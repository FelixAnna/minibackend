using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderItemService
    {
        OrderItem CreateOrderItem(string userId, OrderItemModel item);
        bool RemoveOrderItem(int orderItemId, string userId);
    }
}