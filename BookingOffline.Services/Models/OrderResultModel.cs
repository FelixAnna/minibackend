using BookingOffline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingOffline.Services.Models
{
    public class OrderResultModel
    {
        public string OrderId { get; set; }

        public int? ShopId { get; set; }

        public int State { get; set; }
        public List<OrderItemResultModel> ProductList { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string OwnerName { get; set; }

        public static OrderResultModel FromOrder(Order order, IEnumerable<AlipayUser> users)
        {
            var result = new OrderResultModel()
            {
                OrderId = order.OrderId,
                ShopId = order.ShopId,
                State = order.State,
                ProductList = order.OrderItems?.Select(x => OrderItemResultModel.FromOrderItem(x, users)).OrderByDescending(x=>x.CreatedAt).ToList() ?? new List<OrderItemResultModel>(),
                CreatedAt = order.CreatedAt.ToUniversalTime(),
                CreatedBy = order.CreatedBy,
                OwnerName = users.FirstOrDefault(x => x.Id == order.CreatedBy)?.AlipayName
            };

            return result;
        }
    }

    public class OrderItemResultModel
    {
        public int OrderItemId { get; set; }

        public int? ProductId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Remark { get; set; }

        public List<OrderItemOptionsResultModel> Options { get; set; }

        public DateTime CreatedAt { get; set; }
        public string OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }

        public static OrderItemResultModel FromOrderItem(OrderItem iten, IEnumerable<AlipayUser> users)
        {
            var result = new OrderItemResultModel()
            {
                OrderItemId = iten.OrderItemId,
                ProductId = iten.ProductId,
                Name = iten.Name,
                Price = iten.Price,
                Remark = iten.Remark,
                Options = iten.OrderItemOptions?.Select(OrderItemOptionsResultModel.FromOrderItenOption).ToList() ?? new List<OrderItemOptionsResultModel>(),
                CreatedAt = iten.CreatedAt,
                OwnerId = iten.CreatedBy,
                OwnerAvatar = users.FirstOrDefault(x => x.Id == iten.CreatedBy)?.AlipayPhoto,
                OwnerName = users.FirstOrDefault(x => x.Id == iten.CreatedBy)?.AlipayName
            };

            return result;
        }
    }

    public class OrderItemOptionsResultModel
    {
        public string Name { get; set; }
        public bool Value { get; set; }

        public static OrderItemOptionsResultModel FromOrderItenOption(OrderItemOption option)
        {
            var result = new OrderItemOptionsResultModel()
            {
                Name = option.Name,
                Value = option.Value
            };

            return result;
        }
    }
}
