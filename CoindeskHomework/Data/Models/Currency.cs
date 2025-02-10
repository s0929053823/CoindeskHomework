using System.ComponentModel.DataAnnotations;

namespace CoindeskHomework.Data.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public required string CurrencyCode { get; set; }


        [MaxLength(250)]
        public string? ChineseName { get; set; }


        [MaxLength(500)] 
        public string? Description { get; set; }

        [MaxLength (50)]
        public string? Symbol { get; set; }

    }
}
