using System;

namespace BookingOffline.Entities
{
    public class AlipayUser
    {
        public string Id { get; set; }

        public string AlibabaUserId { get; set; }

        public string AlipayUserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
