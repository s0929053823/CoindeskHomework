using CoindeskHomework.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public class CurrencyRateDto
    {
 
        public string Currency { get; set; }

        public float Rate { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
