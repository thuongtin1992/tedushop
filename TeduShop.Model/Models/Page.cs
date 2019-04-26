using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("Pages")]
    public class Page : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(250)]
        public string Name { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(250)]
        [Required]
        public string Alias { set; get; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

    }
}