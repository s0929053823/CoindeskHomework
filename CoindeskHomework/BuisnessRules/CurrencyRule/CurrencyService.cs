using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.Common;
using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{

    public class CurrencyService : BaseService, ICurrencyService
    {
        public CurrencyService(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return await _context.Currencies
                .OrderBy(c => c.CurrencyCode)
                .ToListAsync();
        }


        public async Task<Currency?> GetCurrencyByIdAsync(int id)
        {

            var currency =  await _context.Currencies.FindAsync(id);
            if (currency == null)
                throw new NotFoundException("幣別不存在");
            return currency;
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
                throw new NotFoundException("幣別不存在"); 


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
                throw new NotFoundException("幣別不存在");

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
