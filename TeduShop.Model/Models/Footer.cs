using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string ID { get; set; }

        [Required]
        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}