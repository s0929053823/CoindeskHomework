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

            var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
            throw new NotImplementedException(); // 這裡應該要回傳 bpiResult
            return Ok(bpiResult); // 回傳整個 BpiResult

        }


        [HttpPost]
        public async Task<IActionResult> ImportCoinDeskToDatabase([FromServices] ICoinDeskImportService coinDeskImportService)
        {

            var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
            await coinDeskImportService.Import(bpiResult);
            return Ok();


        }
    }

}
