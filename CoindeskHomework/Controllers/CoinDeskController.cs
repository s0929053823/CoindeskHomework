using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinDeskController : ControllerBase
    {
        private readonly CoinDeskService _coinDeskService;

        public CoinDeskController(CoinDeskService coinDeskService)
        {
            _coinDeskService = coinDeskService;
        }

        // 取得 Coindesk API 解析後的所有幣別資訊
        [HttpGet]
        public async Task<IActionResult> GetCurrencyInfo()
        {
            try
            {
                var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
                return Ok(bpiResult); // 回傳整個 BpiResult
            }
            catch (Exception ex)
            {
                // 捕捉到錯誤後回傳 BadRequest 以及錯誤訊息
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
