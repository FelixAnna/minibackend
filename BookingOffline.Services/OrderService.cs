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
        private readonly IAlipayUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepo, IOrderItemRepository productRepo, IAlipayUserRepository userRepository, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userRepository = userRepository;
            _logger = logger;
        }

        public Order CreateOrder(string userId, OrderModel order)
        {
            if (_orderRepo.FindByAlipayId(order.OrderId) != null)
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

        public OrderResultModel GetOrder(string orderId)
        {
            var order = _orderRepo.FindByAlipayId(orderId);
            if (order == null)
            {
                return null;
            }

            var users = new List<AlipayUser>();
            var relatedUserIds = order.OrderItems.Select(x => x.CreatedBy).ToList();
            relatedUserIds.Add(order.CreatedBy);
            if (relatedUserIds.Any())
            {
                users = _userRepository.FindAll(relatedUserIds.ToArray()).ToList();
            }

            var result = OrderResultModel.FromOrder(order, users);

            return result;
        }

        public OrderCollectionResultModel GetOrders(string userId)
        {
            var orders = _orderRepo.FindAll(userId);
            var users = new List<AlipayUser>();
            var relatedUserIds = orders.Select(x => x.CreatedBy).ToList();
            if (relatedUserIds.Any())
            {
                users = _userRepository.FindAll(relatedUserIds.ToArray()).ToList();
            }

            var results = new OrderCollectionResultModel()
            {
                TotalCount = orders.Count(),
                Orders = (from od in orders?.Skip(0).Take(10).ToList()
                          let us = users.FirstOrDefault(u => u.Id == od.CreatedBy)
                          select OrderCollectionItem.ToOrderCollectionItem(od, us)).ToList()
            };

            return results;
        }

        public bool RemoveOrder(string orderId, string userId)
        {
            return _orderRepo.Delete(orderId, userId);
        }
    }
}
