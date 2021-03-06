﻿using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingOffline.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ILogger<OrderItemService> _logger;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;

        public OrderItemService(IOrderRepository orderRepo, IOrderItemRepository productRepo, ILogger<OrderItemService> logger)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = productRepo;
            _logger = logger;
        }

        public OrderItem CreateOrderItem(string userId, OrderItemModel item)
        {
            if (_orderRepo.FindById(item.OrderId)?.State == (int)OrderStatus.New)
            {
                var newOrderItem = _orderItemRepo.Create(new OrderItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Remark = item.Remark,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                    OrderId = item.OrderId,
                    OrderItemOptions = item.Options.Select(x => new OrderItemOption()
                    {
                        Name = x.Name,
                        Value = x.Value
                    }).ToList()
                });

                return newOrderItem;
            }
            else
            {
                throw new Exception($"Failed to create orderItem for order: {item.OrderId}");
            }
        }

        public OrderItem GetOrderItem(int orderItemId)
        {
            var item = _orderItemRepo.FindById(orderItemId);
            return item;
        }

        public IList<OrderItem> GetOrderItems(string orderId)
        {
            var orderItems = _orderItemRepo.FindAll(orderId);
            return orderItems?.Skip(0).Take(100).ToList();
        }

        public bool RemoveOrderItem(int orderItemId, string userId)
        {
            var item = _orderItemRepo.FindById(orderItemId);
            if (_orderRepo.FindById(item?.OrderId)?.State == (int)OrderStatus.New
                && _orderItemRepo.Delete(orderItemId, userId))
            {
                return true;
            }
            else
            {
                throw new Exception($"Failed to remove orderItem: {orderItemId}");
            }
        }
    }
}
