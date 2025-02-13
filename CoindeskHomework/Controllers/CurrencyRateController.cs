using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{

    public class CurrencyRateController : BaseApiController
    {
        private readonly CurrencyRateService _currencyRateService;

        public CurrencyRateController(CurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }


        [HttpGet("rate/{currencyCode}")]
        public async Task<ActionResult<List<CurrencyRate>>> GetRates(string currencyCode)
        {
            var rates = (await _currencyRateService.GetRates(currencyCode)).ToList() ;

            if (rates == null)
                return NotFound("找不到該幣別的匯率");

            return Ok(rates);
        }
    }
}
