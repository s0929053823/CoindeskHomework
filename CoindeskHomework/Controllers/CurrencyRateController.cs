using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoindeskHomework.Controllers
{

    public class CurrencyRateController : BaseApiController
    {
        private readonly ApplicationDbContext _context;


        public CurrencyRateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ 查詢所有幣別，依照 CurrencyCode 排序
        [HttpGet("currencies")]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            var currencies = await _context.Currencies
                .OrderBy(c => c.CurrencyCode)
                .ToListAsync();
            return Ok(currencies);
        }

        // 2️⃣ 查詢某幣別的最新匯率
        [HttpGet("rate/{currencyCode}")]
        public async Task<ActionResult<CurrencyRate>> GetLatestRate(string currencyCode)
        {
            var rate = await _context.CurrencyRates
                .Where(cr => cr.Currency.CurrencyCode == currencyCode)
                .OrderByDescending(cr => cr.UpdatedAt)
                .FirstOrDefaultAsync();

            if (rate == null)
                return NotFound("找不到該幣別的匯率");

            return Ok(rate);
        }

        // 3️⃣ 新增匯率
        [HttpPost("rate")]
        public async Task<ActionResult> AddRate([FromBody] CurrencyRate rate)
        {
            var currency = await _context.Currencies.FindAsync(rate.CurrencyId);
            if (currency == null)
                return NotFound("幣別不存在");

            rate.UpdatedAt = DateTime.UtcNow;
            _context.CurrencyRates.Add(rate);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLatestRate), new { currencyCode = currency.CurrencyCode }, rate);
        }

        // 4️⃣ 修改匯率
        [HttpPut("rate/{id}")]
        public async Task<ActionResult> UpdateRate(int id, [FromBody] CurrencyRate updatedRate)
        {
            var rate = await _context.CurrencyRates.FindAsync(id);
            if (rate == null)
                return NotFound("匯率記錄不存在");

            rate.Rate = updatedRate.Rate;
            rate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5️⃣ 刪除匯率
        [HttpDelete("rate/{id}")]
        public async Task<ActionResult> DeleteRate(int id)
        {
            var rate = await _context.CurrencyRates.FindAsync(id);
            if (rate == null)
                return NotFound("匯率記錄不存在");

            _context.CurrencyRates.Remove(rate);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
