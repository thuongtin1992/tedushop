using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        [Required]
        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        [Required]
        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }

        [Required]
        public string PaymentStatus { set; get; }

        [Required]
        public bool Status { set; get; }

        [MaxLength(128)]
        public string CustomerId { set; get; }

        public string BankCode { get; set; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}