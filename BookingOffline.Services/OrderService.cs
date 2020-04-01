using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            var newOrder = _orderRepo.Create(new Order()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                Options = JsonConvert.SerializeObject(order.Options)
            });

            var users = _userRepository.FindAll(new[] { userId }).ToList();
            return OrderResultModel.FromOrder(newOrder, users);
        }

        public OrderResultModel GetOrder(int orderId)
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

        public OrderCollectionResultModel GetOrders(string userId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int size = 10)
        {
            var orders = _orderRepo.FindAll(userId);
            if (startDate.HasValue)
            {
                orders = orders.Where(x => x.CreatedAt > startDate);
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(x => x.CreatedAt < endDate.Value.AddDays(1));
            }

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
                          select OrderCollectionItem.ToOrderCollectionItem(od, us)).OrderByDescending(x => x.CreatedAt).ToList()
            };

            return results;
        }

        public bool RemoveOrder(int orderId, string userId)
        {
            if (_orderRepo.Delete(orderId, userId))
            {
                _logger.LogInformation($"User {userId} deleted the order {orderId}");
                return true;
            }
            else
            {
                throw new Exception($"Failed to remove order: {orderId}");
            }
        }

        public async Task<bool> LockOrderAsync(int orderId, string userId)
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

        public async Task<bool> UnlockOrderAsync(int orderId, string userId)
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
