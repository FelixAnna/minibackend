using System.Collections.Generic;
using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(string userId, OrderModel order);
        OrderResultModel GetOrder(string orderId);
        OrderCollectionResultModel GetOrders(string userId);
        bool RemoveOrder(string orderId, string userId);
    }
}