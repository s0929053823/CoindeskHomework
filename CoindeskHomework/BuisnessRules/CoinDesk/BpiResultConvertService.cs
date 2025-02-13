using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using CoindeskHomework.Data;
using Microsoft.EntityFrameworkCore;

namespace CoindeskHomework.BuisnessRules.CoinDesk
{


    public class BpiResultConvertService : BaseService, IBpiResultConvertService
    {
        public BpiResultConvertService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<CurrencyInfoViewModel>> Convert(BpiResult bpiResult)
        {
            var currencyInfoList = new List<CurrencyInfoViewModel>();

            foreach (var item in bpiResult.bpi)
            {
                var currency = await _context.Currencies
                    .FirstOrDefaultAsync(c => c.CurrencyCode == item.Key);

                if (currency != null)
                {
                    var currencyInfoDto = new CurrencyInfoViewModel
                    {
                        CurrencyCode = item.Key,
                        ChineseName = currency.ChineseName,
                        Rate = item.Value.rate
                    };

                    currencyInfoList.Add(currencyInfoDto);
                }

            }
            return currencyInfoList;
        }
    }
}
