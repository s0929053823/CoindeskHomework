using CoindeskHomework.Data.Models;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public interface ICurrencyRateService
    {
        Task<IEnumerable<CurrencyRate>> GetRates(string currencyCode);
    }
}