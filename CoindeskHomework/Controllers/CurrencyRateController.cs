using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{

    public class CurrencyRateController : BaseApiController
    {
        private readonly ICurrencyRateService _currencyRateService;

        public CurrencyRateController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }


        [HttpGet("rate/{currencyCode}")]
        public async Task<List<CurrencyRateDto>> GetRates(string currencyCode)
        {
            var rates = (await _currencyRateService.GetRates(currencyCode)).ToList() ;
            return rates;
        }
    }
}
