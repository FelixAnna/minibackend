using System;
using System.Collections.Generic;
using System.Text;

namespace BookingOffline.Services.Models
{
    public class OrderItemModel
    {
        public int OrderId { get; set; }
        
        public int? ProductId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Remark { get; set; }

        public OrderItemOptionModel[] Options { get; set; } = new List<OrderItemOptionModel>().ToArray();
    }

    public class OrderItemOptionModel
    {
        public string Name { get; set; }
        public bool Value { get; set; }
    }
}
