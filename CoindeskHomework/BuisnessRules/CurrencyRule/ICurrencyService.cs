using CoindeskHomework.Data.Models;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public interface ICurrencyService
    {
        Task<Currency> AddCurrencyAsync(AddCurrencyDto currency);
        Task<bool> DeleteCurrencyAsync(int id);
        Task<IEnumerable<Currency>> GetCurrenciesAsync();
        Task<Currency?> GetCurrencyByIdAsync(int id);
        Task<bool> UpdateCurrencyAsync(int id, UpdateCurrencyDto updatedCurrency);
    }
}