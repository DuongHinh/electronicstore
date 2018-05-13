using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Phải nhập tên.")]
        [MaxLength(128, ErrorMessage = "Tên không được quá 128 ký tự")]
        public string FirstName { set; get; }

        [MaxLength(128, ErrorMessage = "Tên đệm không được quá 128 ký tự")]
        public string MiddleName { set; get; }

        [Required(ErrorMessage = "Phải nhập họ.")]
        [MaxLength(128, ErrorMessage = "Họ không được quá 128 ký tự")]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Phải nhập tên đăng nhập.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Phải nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { set; get; }

        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        [Required(ErrorMessage = "Phải nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Phải nhập số điện thoại.")]
        public string PhoneNumber { set; get; }
    }
}