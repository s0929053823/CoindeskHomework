using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{
    public class CurrencyController : BaseApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            var currencies = await _currencyService.GetCurrenciesAsync();
            return Ok(currencies);
        }

    
        [HttpGet("{id}")]
        public async Task<Currency> GetCurrency(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);

            return currency;
        }

       
        [HttpPost]
        public async Task<Currency> CreateCurrency([FromBody] AddCurrencyDto currency)
        {
            var createdCurrency = await _currencyService.AddCurrencyAsync(currency);
            return createdCurrency;
        }

   
        [HttpPut("{id}")]
        public async Task UpdateCurrency(int id, [FromBody] UpdateCurrencyDto updatedCurrency)
        {
           await _currencyService.UpdateCurrencyAsync(id, updatedCurrency);
           
        }

   
        [HttpDelete("{id}")]
        public async Task DeleteCurrency(int id)
        {
           await _currencyService.DeleteCurrencyAsync(id);
        }
    }
}
