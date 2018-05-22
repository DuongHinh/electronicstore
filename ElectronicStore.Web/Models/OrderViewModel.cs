using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Web.Models
{
    public class OrderViewModel
    {
        public int Id { set; get; }

        [MaxLength(256, ErrorMessage = "Tên không được quá 256 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        [MaxLength(256, ErrorMessage = "Địa chỉ không được quá 256 ký tự")]
        public string Address { set; get; }


        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [MaxLength(256, ErrorMessage = "Email không được quá 256 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 50 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string PhoneNumber { set; get; }

        public DateTime? OrderDate { set; get; }

        public DateTime? ShipDate { set; get; }

        public PaymentStatus PaymentStatus { set; get; }

        public ShipStatus ShipStatus { set; get; }

        public OrderStatus Status { set; get; }

        [StringLength(128)]
        public string CustomerId { set; get; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }

        public List<CartItemViewModel> Cart { set; get; }
    }
}