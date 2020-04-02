using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderService
    {
        OrderResultModel CreateOrder(string userId, OrderModel order);
        OrderResultModel GetOrder(int orderId);
        OrderCollectionResultModel GetOrders(string userId, 
            DateTime? startDate, 
            DateTime? endDate, 
            int page = 1, 
            int size = 10);
        bool RemoveOrder(int orderId, string userId);
        Task<bool> UnlockOrderAsync(int orderId, string userId);
        Task<bool> LockOrderAsync(int orderId, string userId);
        Task<OrderResultModel> UpdateOrderAsync(int id, string userId, OrderModel model);
    }
}