using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Tên đại diện")]
        [Required(ErrorMessage = "Yêu cầu nhập {0}.")]
        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Điện thoại")]
        [MaxLength(50, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "{0} chưa đúng định dạng mail.")]
        [MaxLength(50, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        public string Email { get; set; }

        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        public string Website { get; set; }

        [MaxLength(250, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        public string Address { get; set; }

        [Display(Name = "Mục khác")]
        public string Other { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Kinh độ")]
        public double Latitude { get; set; }

        [Display(Name = "Vĩ độ")]
        public double Longitude { get; set; }

        [Display(Name = "Nhúng mã")]
        public string Embed { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}