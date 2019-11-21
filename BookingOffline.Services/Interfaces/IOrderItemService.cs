using System.Collections.Generic;
using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderItemService
    {
        OrderItem CreateOrderItem(string userId, OrderItemModel item);
        OrderItem GetOrderItem(int orderItemId);
        IList<OrderItem> GetOrderItems(string orderId);
        bool RemoveOrderItem(int orderItemId, string userId);
    }
}