using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Service.Projection
{
    public class OrderProjection
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ShipStatus ShipStatus { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<ProductOrderProjection> Products { get; set; }
        public IEnumerable<int> Quantities { get; set; }
        public IEnumerable<decimal> Prices { get; set; }
    }

    public class ProductOrderProjection
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
