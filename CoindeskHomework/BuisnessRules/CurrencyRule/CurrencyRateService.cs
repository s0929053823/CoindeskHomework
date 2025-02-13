using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CoindeskHomework.BuisnessRules.CurrencyRule
{
    public class CurrencyRateService : BaseService, ICurrencyRateService
    {
        public CurrencyRateService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurrencyRateDto>> GetRates(string currencyCode)
        {
            var rates = await _context.CurrencyRate
                .Where(cr => cr.Currency.CurrencyCode == currencyCode)
                .OrderByDescending(cr => cr.UpdatedAt)
             .Select(x => new CurrencyRateDto
             {
                 Currency = x.Currency.CurrencyCode,
                 Rate = x.Rate,
                 UpdatedAt = x.UpdatedAt
             }).ToListAsync();

            return rates;
        }

    }
}
