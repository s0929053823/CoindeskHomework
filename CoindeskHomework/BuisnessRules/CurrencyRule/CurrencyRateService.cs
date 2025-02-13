using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public class CurrencyRateService : BaseService
    {
        public CurrencyRateService(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<CurrencyRate>> GetRates(string currencyCode)
        {
            var rates = await _context.CurrencyRate
                .Where(cr => cr.Currency.CurrencyCode == currencyCode)
                .OrderByDescending(cr => cr.UpdatedAt)
                .ToListAsync();

            return rates;
                    
        }

    }
}
