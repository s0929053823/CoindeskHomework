using CoindeskHomework.BuisnessRules.CoinDesk;
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


        [HttpPost]
        public async Task<IActionResult> ImportCoinDeskToDatabase([FromServices] ICoinDeskImportService coinDeskImportService)
        {
            try
            {
                var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
                await coinDeskImportService.Import(bpiResult);
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
