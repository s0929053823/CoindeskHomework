using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace CoindeskHomework_Test.BuisnessRules
{
    public class CoinDeskServiceTests
    {
        [Fact]
        public async Task GetCurrencyInfoAsync_ShouldReturnBpiResult_WhenApiCallIsSuccessful()
        {
            // Arrange
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();

            // 設定 mock 的 HttpMessageHandler，當發送請求時返回我們定義的回應
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ \"time\": { \"updated\": \"2025/02/10 00:00:00\" }, \"bpi\": { \"USD\": { \"rate\": \"50000.00\" }}}", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var mockApiSettings = new Mock<IOptions<ApiSettings>>();
            var apiSettings = new ApiSettings
            {
                CoindeskApiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json"
            };
            mockApiSettings.Setup(x => x.Value).Returns(apiSettings);

            var coinDeskService = new CoinDeskService(httpClient , mockApiSettings.Object);

          
            // Act
            var result = await coinDeskService.GetCurrencyInfoAsync();

            var usdRate = result.bpi["USD"].rate;
            // Assert
            Assert.NotNull(result);
            Assert.Equal("50000.00", usdRate); 

        }

        [Fact]
        public async Task GetCurrencyInfoAsync_ShouldThrowException_WhenApiCallFails()
        {
            // Arrange
            var mockApiSettings = new Mock<IOptions<ApiSettings>>();
            var apiSettings = new ApiSettings
            {
                CoindeskApiUrl = "https://apSSSi.coindesk.com/v1/bpi/currentprice.json"
            };
            mockApiSettings.Setup(x => x.Value).Returns(apiSettings);

            var mockHandler = new Mock<HttpMessageHandler>();
            // 模擬 API 呼叫失敗
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Internal Server Error")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var coinDeskService = new CoinDeskService(httpClient, mockApiSettings.Object);

               // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => coinDeskService.GetCurrencyInfoAsync());
      
        }


        [Fact]
        public async Task GetCurrencyInfoAsync_ShouldThrowException__WhenApiReturnsInValidResponse()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();

            // 設定 mock 的 HttpMessageHandler，當發送請求時返回我們定義的回應
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var mockApiSettings = new Mock<IOptions<ApiSettings>>();
            var apiSettings = new ApiSettings
            {
                CoindeskApiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json"
            };
            mockApiSettings.Setup(x => x.Value).Returns(apiSettings);

            var coinDeskService = new CoinDeskService(httpClient, mockApiSettings.Object);

            // Act&Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => coinDeskService.GetCurrencyInfoAsync());

        }


        [Fact]
        public async Task GetCurrencyInfoAsync_ShouldReturnNonFakeData_WhenApiReturnsValidResponse2()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();

            // 設定 mock 的 HttpMessageHandler，當發送請求時返回我們定義的回應
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ \"time\": { \"updated\": \"2025/02/10 00:00:00\" }, \"bpi\": { \"USD\": { \"rate\": \"50.00\" }}}", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var mockApiSettings = new Mock<IOptions<ApiSettings>>();
            var apiSettings = new ApiSettings
            {
                CoindeskApiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json"
            };
            mockApiSettings.Setup(x => x.Value).Returns(apiSettings);

            var coinDeskService = new CoinDeskService(httpClient, mockApiSettings.Object);


            // Act
            var result = await coinDeskService.GetCurrencyInfoAsync();
            var usdRate = result.bpi["USD"].rate;
            // Assert
            Assert.NotNull(result);
            Assert.NotEqual("50000.00", usdRate);

        }
    }
}