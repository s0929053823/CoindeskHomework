using CoindeskHomework.BuisnessRules.CoinDesk;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace CoindeskHomework_Test.BuisnessRules
{
    public class BpiResultConvertServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BpiResultConvertService _service;

        public BpiResultConvertServiceTests()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            _dbContext = new ApplicationDbContext(options);


            _service = new BpiResultConvertService(_dbContext);
        }

        [Fact]
        public async Task Convert_ShouldReturnCurrencyInfoList_WhenBpiResultIsValid()
        {
            // Arrange
            var bpiResult = new BpiResult
            {
                bpi = new Dictionary<string, BpiResult.CurrencyInfo>
            {
                { "USD", new BpiResult.CurrencyInfo { rate = "99,102.526" } },
                { "GBP", new BpiResult.CurrencyInfo { rate = "79,587.951" } }
            }
            };


            _dbContext.Currencies.AddRange(
                new Currency { CurrencyCode = "USD", ChineseName = "美金" },
                new Currency { CurrencyCode = "GBP", ChineseName = "英鎊" }
            );
            _dbContext.SaveChanges();

            // Act
            var result = await _service.Convert(bpiResult);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("USD", result[0].CurrencyCode);
            Assert.Equal("美金", result[0].ChineseName);
            Assert.Equal("99,102.526", result[0].Rate);

            Assert.Equal("GBP", result[1].CurrencyCode);
            Assert.Equal("英鎊", result[1].ChineseName);
            Assert.Equal("79,587.951", result[1].Rate);
        }

      
    }
}