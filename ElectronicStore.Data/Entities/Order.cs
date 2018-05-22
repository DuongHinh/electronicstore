using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Address { set; get; }

        [MaxLength(256)]
        public string Email { set; get; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { set; get; }

        public DateTime? OrderDate { set; get; }

        public DateTime? ShipDate { set; get; }

        public PaymentStatus PaymentStatus { set; get; }

        public ShipStatus ShipStatus { set; get; }

        public OrderStatus Status { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string CustomerId { set; get; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { set; get; }


        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
    }
}