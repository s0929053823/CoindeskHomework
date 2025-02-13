using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoindeskHomework.Data.Models
{
    public class CurrencyRate
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CurrencyId { get; set; } 

        [ForeignKey("CurrencyId")]
        public required Currency Currency { get; set; }

        [Required]
        public float Rate { get; set; } 

        public DateTime UpdatedAt { get; set; } 
    }
}
