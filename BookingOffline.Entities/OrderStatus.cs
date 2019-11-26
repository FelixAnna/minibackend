using System;
using System.Collections.Generic;
using System.Text;

namespace BookingOffline.Entities
{
    public enum OrderStatus
    {
        New=1,
        Locked=2,
        Deleted=255
    }
}
