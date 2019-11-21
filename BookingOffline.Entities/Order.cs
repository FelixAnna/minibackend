using System;
using System.Collections.Generic;

namespace BookingOffline.Entities
{
    public class Order
    {
        public string OrderId { get; set; }

        public int? ShopId { get; set; }

        public int State { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
