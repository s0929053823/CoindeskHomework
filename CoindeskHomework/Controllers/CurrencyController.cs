using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{
    public class CurrencyController : BaseApiController
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
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
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
                return NotFound("幣別不存在");

            return Ok(currency);
        }

       
        [HttpPost]
        public async Task<ActionResult<Currency>> CreateCurrency([FromBody] AddCurrencyDto currency)
        {
            var createdCurrency = await _currencyService.AddCurrencyAsync(currency);
            return CreatedAtAction(nameof(GetCurrency), new { id = createdCurrency.Id }, createdCurrency);
        }

   
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCurrency(int id, [FromBody] UpdateCurrencyDto updatedCurrency)
        {
            var success = await _currencyService.UpdateCurrencyAsync(id, updatedCurrency);
            if (!success)
                return NotFound("幣別不存在");

            return NoContent();
        }

   
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            var success = await _currencyService.DeleteCurrencyAsync(id);
            if (!success)
                return NotFound("幣別不存在");

            return NoContent();
        }
    }
}
