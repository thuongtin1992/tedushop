using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }   

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string UpdatedBy { get; set; }

        [MaxLength(250)]
        public string MetaKeyword { get; set; }

        [MaxLength(250)]
        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}
