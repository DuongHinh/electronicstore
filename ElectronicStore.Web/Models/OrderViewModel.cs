using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class OrderViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerPhone { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public DateTime? OrderDate { set; get; }

        public DateTime? ShipDate { set; get; }

        public bool IsPaymented { set; get; }

        public bool IsShiped { set; get; }

        public virtual IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}