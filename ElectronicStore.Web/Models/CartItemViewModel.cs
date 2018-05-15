using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    [Serializable]
    public class CartItemViewModel
    {
        public ProductViewModel Product { set; get; }
        public int Quantity { set; get; }
    }
}