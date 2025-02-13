namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public class AddCurrencyDto
    {
  
        public string CurrencyCode { get; set; } = string.Empty;
        public string? ChineseName { get; set; }
        public string? Description { get; set; }
        public string? Symbol { get; set; }
    }
}
