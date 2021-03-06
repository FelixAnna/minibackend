﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookingOffline.Entities;
using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface IOrderService
    {
        OrderResultModel CreateOrder(string userId, OrderModel order);
        OrderResultModel GetOrder(string orderId);
        OrderCollectionResultModel GetOrders(string userId, int page = 1, int size = 10);
        bool RemoveOrder(string orderId, string userId);
        Task<bool> UnlockOrder(string orderId, string userId);
        Task<bool> LockOrder(string orderId, string userId);
    }
}