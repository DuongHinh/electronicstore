using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities.Enum
{
    public class OrderEnum
    {
        public enum PaymentStatus
        {
            Pending = 0,
            Paid = 1,
            Cancelled = 2
        }

        public enum ShipStatus
        {
            Pending = 0,
            Shipping = 1,
            Shipped = 2,
            Cancelled = 3
        }

        public enum OrderStatus
        {
            Pending = 0,
            Done = 1,
            Cancelled = 3
        }
    }
}
