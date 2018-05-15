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

        [MaxLength(256, ErrorMessage = "Tên không được quá 256 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        public string CustomerName { set; get; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        [MaxLength(256, ErrorMessage = "Địa chỉ không được quá 256 ký tự")]
        public string CustomerAddress { set; get; }

        [MaxLength(256, ErrorMessage = "Email không được quá 256 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string CustomerEmail { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 50 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string CustomerPhone { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public DateTime? OrderDate { set; get; }

        public DateTime? ShipDate { set; get; }

        public bool IsPaymented { set; get; }

        public bool IsShiped { set; get; }

        public List<CartItemViewModel> Cart { set; get; }
    }
}