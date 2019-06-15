using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Website { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(300)]
        public string Other { get; set; }

        public bool Status { get; set; }

        [DefaultValue(0)]
        public double Latitude { get; set; }

        [DefaultValue(0)]
        public double Longitude { get; set; }

        public string Embed { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
