using Microsoft.EntityFrameworkCore;
using CoindeskHomework.BuisnessRules.CoinDesk;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using CoindeskHomework.Data.Models;
using CoindeskHomework.Data;
namespace CoindeskHomework_Test.BuisnessRules
{
    public class CoinDeskImportServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CoinDeskImportService _service;

        public CoinDeskImportServiceTests()
        {
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

 
            _dbContext = new ApplicationDbContext(options);

    
            _service = new CoinDeskImportService(_dbContext);
        }

        [Fact]
        public async Task Import_ShouldAddCurrencyAndCurrencyRate_WhenCurrencyNotExist()
        {
            // Arrange
            var bpiResult = new BpiResult
            {
                time = new BpiResult.Time
                {
                    updatedISO = System.DateTime.UtcNow
                },
                bpi = new Dictionary<string, BpiResult.CurrencyInfo>
            {
                { "USD", new BpiResult.CurrencyInfo { rate_float = 123.45f, description = "US Dollar", symbol = "$" } }
            }
            };

            // Act
            await _service.Import(bpiResult);

            // Assert
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(c => c.CurrencyCode == "USD");
            Assert.NotNull(currency);
            Assert.Equal("USD", currency.CurrencyCode);
            Assert.Equal("US Dollar", currency.Description);

            var currencyRate = await _dbContext.CurrencyRate.FirstOrDefaultAsync(cr => cr.CurrencyId == currency.Id);
            Assert.NotNull(currencyRate);
            Assert.Equal(123.45f, currencyRate.Rate);
        }

        [Fact]
        public async Task Import_ShouldUpdateCurrencyRate_WhenCurrencyExists()
        {
            // Arrange
            var existingCurrency = new Currency
            {
                CurrencyCode = "USD",
                ChineseName = "美金",
                Description = "US Dollar",
                Symbol = "$"
            };

            _dbContext.Currencies.Add(existingCurrency);
            await _dbContext.SaveChangesAsync();

            var bpiResult = new BpiResult
            {
                time = new BpiResult.Time
                {
                    updatedISO = System.DateTime.UtcNow
                },
                bpi = new Dictionary<string, BpiResult.CurrencyInfo>
            {
                { "USD", new BpiResult.CurrencyInfo { rate_float = 234.56f, description = "US Dollar", symbol = "$" } }
            }
            };

            // Act
            await _service.Import(bpiResult);

            // Assert
            var currencyRate = await _dbContext.CurrencyRate.FirstOrDefaultAsync(cr => cr.CurrencyId == existingCurrency.Id);
            Assert.NotNull(currencyRate);
            Assert.Equal(234.56f, currencyRate.Rate);
        }

        [Fact]
        public async Task Import_ShouldNotAddCurrency_WhenCurrencyExists()
        {
            // Arrange
            var existingCurrency = new Currency
            {
                CurrencyCode = "USD",
                ChineseName = "美金",
                Description = "US Dollar",
                Symbol = "$"
            };

            _dbContext.Currencies.Add(existingCurrency);
            await _dbContext.SaveChangesAsync();

            var bpiResult = new BpiResult
            {
                time = new BpiResult.Time
                {
                    updatedISO = System.DateTime.UtcNow
                },
                bpi = new Dictionary<string, BpiResult.CurrencyInfo>
            {
                { "USD", new BpiResult.CurrencyInfo { rate_float = 123.45f, description = "US Dollar", symbol = "$" } }
            }
            };

            // Act
            await _service.Import(bpiResult);

            // Assert
            var currencies = await _dbContext.Currencies.ToListAsync();
            Assert.Single(currencies);
        }
    }
}