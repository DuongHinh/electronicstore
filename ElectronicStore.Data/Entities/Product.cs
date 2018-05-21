using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicStore.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Alias { set; get; }

        public int CategoryId { set; get; }

        public int BrandId { set; get; }

        public string Image { set; get; }

        [Column(TypeName = "ntext")]
        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal OriginalPrice { set; get; }

        public int Quantity { set; get; }

        public decimal? PromotionPrice { set; get; }

        public string Description { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public DateTime? CreatedDate { set; get; }

        [MaxLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { set; get; }

        public string Tags { set; get; }

        public virtual IEnumerable<ProductTag> ProductTag { set; get; }
    }
}