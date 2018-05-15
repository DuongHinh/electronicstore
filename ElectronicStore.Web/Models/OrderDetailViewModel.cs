using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderId { set; get; }

        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public virtual OrderViewModel Order { set; get; }
    }
}