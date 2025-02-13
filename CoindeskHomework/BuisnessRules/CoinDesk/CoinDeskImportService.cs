using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoindeskHomework.BuisnessRules.CoinDesk
{
    public class CoinDeskImportService : BaseService, ICoinDeskImportService
    {
        public CoinDeskImportService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Import(BpiResult bpiResult)
        {
            if (bpiResult == null)
                return;

            foreach (var kvp in bpiResult.bpi)
            {
                var currencyCode = kvp.Key;
                var currencyInfo = kvp.Value;


                var currency = await _context.Currencies
                    .Include(c => c.CurrencyRates)
                    .FirstOrDefaultAsync(c => c.CurrencyCode == currencyCode);

                if (currency == null)
                {
                    currency = new Currency
                    {
                        CurrencyCode = currencyCode,
                        ChineseName = "",
                        Description = currencyInfo.description,
                        Symbol = currencyInfo.symbol
                    };

                    _context.Currencies.Add(currency);
                    await _context.SaveChangesAsync();
                }


                var currencyRate = new CurrencyRate
                {
                    CurrencyId = currency.Id,
                    Rate = currencyInfo.rate_float,
                    UpdatedAt = bpiResult.time.updatedISO
                };

                _context.CurrencyRate.Add(currencyRate);
            }

            // 4. 儲存所有變更
            await _context.SaveChangesAsync();

        }
    }
}
