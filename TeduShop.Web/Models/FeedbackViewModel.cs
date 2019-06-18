using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }

        [Display(Name = "Tên")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string LastName { get; set; }

        [Display(Name = "Họ & tên")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        [Required(ErrorMessage = "{0} phải nhập")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "{0} phải nhập")]
        [EmailAddress(ErrorMessage = "{0} không đúng định dạng")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string Email { set; get; }

        [Display(Name = "Điện thoại")]
        [MaxLength(50, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string Address { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "{0} phải nhập")]
        [MaxLength(500, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}