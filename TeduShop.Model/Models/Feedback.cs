using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [MaxLength(250)]
        [Required]
        public string FullName { set; get; }

        [MaxLength(250)]
        public string Email { set; get; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(500)]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}