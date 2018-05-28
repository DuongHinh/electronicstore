using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class BrandViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public bool Status { set; get; }

        public string Description { set; get; }

        public string Logo { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}