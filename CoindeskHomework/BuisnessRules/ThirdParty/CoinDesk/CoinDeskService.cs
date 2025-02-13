using CoindeskHomework.BuisnessRules.Common;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk
{
    public class CoinDeskService : ICoinDeskService
    {
        private readonly HttpClient _httpClient;
        private readonly string _coinDeskUrl;

        public CoinDeskService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _coinDeskUrl = apiSettings.Value.CoindeskApiUrl;
        }
        public async Task<BpiResult?> GetCurrencyInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_coinDeskUrl);
                var bpiResult = JsonSerializer.Deserialize<BpiResult>(response);
                return bpiResult ?? throw new Exception("Failed to deserialize BpiResult");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }
}
