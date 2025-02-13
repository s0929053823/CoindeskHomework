using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{

    public class CurrencyService
    {
        private readonly ApplicationDbContext _context;

        public CurrencyService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return await _context.Currencies
                .OrderBy(c => c.CurrencyCode)
                .ToListAsync();
        }

 
        public async Task<Currency?> GetCurrencyByIdAsync(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

   
        public async Task<Currency> AddCurrencyAsync(AddCurrencyDto currency)
        {
            var newCurrency = new Currency
            {
                CurrencyCode = currency.CurrencyCode,
                ChineseName = currency.ChineseName,
                Description = currency.Description,
                Symbol = currency.Symbol
            };

            _context.Currencies.Add(newCurrency);
            await _context.SaveChangesAsync();
            return newCurrency;
        }

        public async Task<bool> UpdateCurrencyAsync(int id, UpdateCurrencyDto updatedCurrency)
        {
            var existingCurrency = await _context.Currencies.FindAsync(id);
            if (existingCurrency == null)
                return false;
      
            existingCurrency.ChineseName = updatedCurrency.ChineseName;
            existingCurrency.Description = updatedCurrency.Description;
            existingCurrency.Symbol = updatedCurrency.Symbol;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCurrencyAsync(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
                return false;

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
