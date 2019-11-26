using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepo;
        private readonly IAlipayUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepo, IAlipayUserRepository userRepository, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _userRepository = userRepository;
            _logger = logger;
        }

        public OrderResultModel CreateOrder(string userId, OrderModel order)
        {
            if (_orderRepo.FindById(order.OrderId) != null)
            {
                throw new Exception($"Order already exists: {order.OrderId}");
            }

            var newOrder = _orderRepo.Create(new Order()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                OrderId = order.OrderId,
                ShopId = order.ShopId
            });

            var users = _userRepository.FindAll(new[] { userId }).ToList();
            return OrderResultModel.FromOrder(newOrder, users);
        }

        public OrderResultModel GetOrder(string orderId)
        {
            var order = _orderRepo.FindById(orderId);
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

        public OrderCollectionResultModel GetOrders(string userId, int page = 1, int size = 10)
        {
            var orders = _orderRepo.FindAll(userId).ToArray();

            var returnedOrders = orders?.Skip((page - 1) * size).Take(size).ToList();
            var users = new List<AlipayUser>();
            var relatedUserIds = returnedOrders.Select(x => x.CreatedBy).ToList();
            if (relatedUserIds.Any())
            {
                users = _userRepository.FindAll(relatedUserIds.ToArray()).ToList();
            }

            var results = new OrderCollectionResultModel()
            {
                TotalCount = orders.Count(),
                Orders = (from od in returnedOrders
                          let us = users.FirstOrDefault(u => u.Id == od.CreatedBy)
                          select OrderCollectionItem.ToOrderCollectionItem(od, us)).ToList()
            };

            return results;
        }

        public bool RemoveOrder(string orderId, string userId)
        {
            return _orderRepo.Delete(orderId, userId);
        }

        public async Task<bool> LockOrder(string orderId, string userId)
        {
            var order = _orderRepo.FindById(orderId);
            if (order == null)
            {
                return false;
            }

            await _orderRepo.LockOrderAsync(order);
            _logger.LogInformation($"User {userId} locked the order {orderId}");
            return true;
        }

        public async Task<bool> UnlockOrder(string orderId, string userId)
        {
            var order = _orderRepo.FindById(orderId);
            if (order == null)
            {
                return false;
            }

            await _orderRepo.UnlockOrderAsync(order);
            _logger.LogInformation($"User {userId} unlocked the order {orderId}");
            return true;
        }
    }
}
