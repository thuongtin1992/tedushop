using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Họ & tên")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        public string FullName { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [MinLength(6, ErrorMessage = "{0} phải có ít nhất {1} ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "{0} không đúng định dạng")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "{0} phải nhập")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
}