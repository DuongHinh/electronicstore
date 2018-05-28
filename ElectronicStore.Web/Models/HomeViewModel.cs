using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> NewArrivalProducts { set; get; }
        public IEnumerable<ProductViewModel> HotProducts { set; get; }
        public IEnumerable<BrandViewModel> Brands { set; get; }
    }
}