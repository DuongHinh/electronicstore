using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    [Serializable]
    public class ProductViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public int CategoryId { set; get; }

        public string Image { set; get; }

        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal OriginalPrice { set; get; }

        public int Quantity { set; get; }

        public decimal? PromotionPrice { set; get; }

        public string Description { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        public virtual ProductCategoryViewModel ProductCategory { set; get; }

        public virtual IEnumerable<ProductTagViewModel> ProductTag { set; get; }
    }
}