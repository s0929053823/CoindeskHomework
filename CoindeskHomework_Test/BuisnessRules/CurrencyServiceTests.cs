using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.Data.Models;
using CoindeskHomework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoindeskHomework_Test.BuisnessRules
{
    public class CurrencyServiceTests
    {

        private readonly ApplicationDbContext _context;
        private readonly CurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _currencyService = new CurrencyService(_context);
        }

        [Fact]
        public async Task AddCurrencyAsync_ShouldAddCurrency()
        {
            // Arrange
            var newCurrency = new AddCurrencyDto
            {
                CurrencyCode = "USD",
                ChineseName = "美金",
                Description = "美國貨幣",
                Symbol = "$"
            };

            // Act
            var result = await _currencyService.AddCurrencyAsync(newCurrency);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.CurrencyCode);
            Assert.Equal("美金", result.ChineseName);
        }

        [Fact]
        public async Task GetCurrenciesAsync_ShouldReturnAllCurrencies()
        {
            // Arrange
            _context.Currencies.AddRange(new List<Currency>
        {
            new Currency { CurrencyCode = "USD", ChineseName = "美金", Description = "美國貨幣", Symbol = "$" },
            new Currency { CurrencyCode = "EUR", ChineseName = "歐元", Description = "歐洲貨幣", Symbol = "€" }
        });
            await _context.SaveChangesAsync();

            // Act
            var result = await _currencyService.GetCurrenciesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCurrencyByIdAsync_ShouldReturnCorrectCurrency()
        {
            // Arrange
            var currency = new Currency { CurrencyCode = "JPY", ChineseName = "日圓", Description = "日本貨幣", Symbol = "¥" };
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            // Act
            var result = await _currencyService.GetCurrencyByIdAsync(currency.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("JPY", result.CurrencyCode);
        }

        [Fact]
        public async Task UpdateCurrencyAsync_ShouldModifyExistingCurrency()
        {
            // Arrange
            var currency = new Currency { CurrencyCode = "GBP", ChineseName = "英鎊", Description = "英國貨幣", Symbol = "£" };
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            var updatedCurrency = new UpdateCurrencyDto
            {
                ChineseName = "英鎊更新",
                Description = "更新後的英鎊貨幣",
                Symbol = "££"
            };

            // Act
            var result = await _currencyService.UpdateCurrencyAsync(currency.Id, updatedCurrency);
            var updated = await _context.Currencies.FindAsync(currency.Id);

            // Assert
            Assert.True(result);
            Assert.Equal("英鎊更新", updated?.ChineseName);
        }

        [Fact]
        public async Task DeleteCurrencyAsync_ShouldRemoveCurrency()
        {
            // Arrange
            var currency = new Currency { CurrencyCode = "AUD", ChineseName = "澳幣", Description = "澳洲貨幣", Symbol = "A$" };
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            // Act
            var result = await _currencyService.DeleteCurrencyAsync(currency.Id);
            var deleted = await _context.Currencies.FindAsync(currency.Id);

            // Assert
            Assert.True(result);
            Assert.Null(deleted);
        }
    }
}

