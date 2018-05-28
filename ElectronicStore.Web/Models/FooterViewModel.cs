using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class FooterViewModel
    {
        public ContactViewModel ContactInfo { set; get; }

        public IEnumerable<ProductCategoryViewModel> ProductCategories { set; get; }
    }
}