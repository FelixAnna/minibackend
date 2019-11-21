using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingOffline.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _productRepo;

        public OrderService(IOrderRepository orderRepo, IOrderItemRepository productRepo, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _logger = logger;
        }

        public Order CreateOrder(string userId, OrderModel order)
        {
            if (_orderRepo.FindById(order.OrderId) != null)
            {
                throw new Exception($"Order already exists: {order.OrderId}");
            }

            var newOrder = _orderRepo.Create(new Order()
            {
                State = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                OrderId = order.OrderId,
                ShopId = order.ShopId
            });

            return newOrder;
        }

        public Order GetOrder(string orderId)
        {
            var order = _orderRepo.FindById(orderId);
            return order;
        }

        public IList<Order> GetOrders(string userId)
        {
            var orders = _orderRepo.FindAll(userId);
            return orders?.Skip(0).Take(10).ToList();
        }

        public bool RemoveOrder(string orderId, string userId)
        {
            return _orderRepo.Delete(orderId, userId);
        }
    }
}
