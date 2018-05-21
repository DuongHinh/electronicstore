using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class FeedbackViewModel
    {
        public int Id { set; get; }

        [MaxLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 250 ký tự")]
        public string PhoneNumber { set; get; }

        [MaxLength(500, ErrorMessage = "Tin nhắn không được quá 500 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tin nhắn")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        public bool IsRead { set; get; }

        public ContactViewModel ContactInfor { set; get; }
    }
}