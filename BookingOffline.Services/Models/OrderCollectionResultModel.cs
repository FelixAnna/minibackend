using BookingOffline.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingOffline.Services.Models
{
    public class OrderCollectionResultModel
    {
        public int TotalCount { get; set; }

        public List<OrderCollectionItem> Orders { get; set; }
    }

    public class OrderCollectionItem
    {
        public string OrderId { get; set; }

        public int? ShopId { get; set; }

        public int State { get; set; }
        public int TotalItems { get; set; }

        public DateTime CreatedAt { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }

        public static OrderCollectionItem ToOrderCollectionItem(Order order, AlipayUser user)
        {
            return new OrderCollectionItem()
            {
                OrderId = order.OrderId,
                ShopId = order.ShopId,
                State = order.State,
                TotalItems = order.OrderItems?.Count ?? 0,
                CreatedAt = order.CreatedAt,
                OwnerId = order.CreatedBy,
                OwnerName = user?.AlipayName
            };
        }
    }
}
