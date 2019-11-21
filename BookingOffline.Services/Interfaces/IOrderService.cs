using System.Collections.Generic;
using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(string userId, OrderModel order);
        Order GetOrder(string orderId);
        IList<Order> GetOrders(string userId);
        bool RemoveOrder(string orderId, string userId);
    }
}