using System;
using System.Collections.Generic;

namespace BookingOffline.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int? ProductId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Remark { get; set; }

        public virtual List<OrderItemOption> OrderItemOptions { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
