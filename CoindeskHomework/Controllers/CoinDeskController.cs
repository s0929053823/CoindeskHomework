using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{

    public class CoinDeskController : BaseApiController
    {
        private readonly ICoinDeskService _coinDeskService;

        public CoinDeskController(ICoinDeskService coinDeskService)
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
