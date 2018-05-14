using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}