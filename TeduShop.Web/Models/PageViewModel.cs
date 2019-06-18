using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class PageViewModel
    {
        public int Index { get; set; }

        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Alias { set; get; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        [Required]
        public bool Status { get; set; }

        public int? ViewCount { get; set; }
    }
}