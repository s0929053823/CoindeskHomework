using CoindeskHomework.Data.Models;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public interface ICurrencyRateService
    {
        Task<IEnumerable<CurrencyRateDto>> GetRates(string currencyCode);
    }
}