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
        public async Task<BpiResult> GetCurrencyInfo()
        {

            var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
            return bpiResult;
        }


        [HttpPost]
        public async Task ImportCoinDeskToDatabase([FromServices] ICoinDeskImportService coinDeskImportService)
        {

            var bpiResult = await _coinDeskService.GetCurrencyInfoAsync();
            await coinDeskImportService.Import(bpiResult);
        }
    }

}
