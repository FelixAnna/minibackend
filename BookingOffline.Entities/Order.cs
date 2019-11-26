using System;
using System.Collections.Generic;

namespace BookingOffline.Entities
{
    public class Order
    {
        public Order()
        {
            State = (int)OrderStatus.New;
        }

        public string OrderId { get; set; }

        public int? ShopId { get; set; }

        public int State { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
