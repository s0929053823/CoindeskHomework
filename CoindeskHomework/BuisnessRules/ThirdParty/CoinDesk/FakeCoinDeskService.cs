using CoindeskHomework.BuisnessRules.Common;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk
{
    public class FakeCoinDeskService : ICoinDeskService
    {
        public async Task<BpiResult?> GetCurrencyInfoAsync()
        {
            var response = @"{
  ""time"": {
    ""updated"": ""Feb 6, 2025 14:23:41 UTC"",
    ""updatedISO"": ""2025-02-06T14:23:41+00:00"",
    ""updateduk"": ""Feb 6, 2025 at 14:23 GMT""
  },
  ""disclaimer"": ""This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org"",
  ""chartName"": ""Bitcoin"",
  ""bpi"": {
    ""USD"": {
      ""code"": ""USD"",
      ""symbol"": ""&#36;"",
      ""rate"": ""99,102.526"",
      ""description"": ""United States Dollar"",
      ""rate_float"": 99102.5263
    },
    ""GBP"": {
      ""code"": ""GBP"",
      ""symbol"": ""&pound;"",
      ""rate"": ""79,587.951"",
      ""description"": ""British Pound Sterling"",
      ""rate_float"": 79587.9505
    },
    ""EUR"": {
      ""code"": ""EUR"",
      ""symbol"": ""&euro;"",
      ""rate"": ""94,749.646"",
      ""description"": ""Euro"",
      ""rate_float"": 94749.646
    }
  }
}";

            return JsonSerializer.Deserialize<BpiResult?>(response);
        }
    }
}
