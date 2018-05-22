using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Web.Models
{
    public class UpdateOrderViewModel
    {
        public int Id { set; get; }

        public DateTime? ShipDate { set; get; }

        public PaymentStatus PaymentStatus { set; get; }

        public ShipStatus ShipStatus { set; get; }

        public OrderStatus Status { set; get; }

    }
}