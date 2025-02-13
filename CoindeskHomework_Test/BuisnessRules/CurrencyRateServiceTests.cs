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
    public class CurrencyRateServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrencyRateService _currencyRateService;

        public CurrencyRateServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _currencyRateService = new CurrencyRateService(_context);
        }

        [Fact]
        public async Task GetRates_ShouldReturnRates_ForValidCurrencyCode()
        {
            // Arrange
            var currency = new Currency { CurrencyCode = "USD", ChineseName = "美金", Description = "美國貨幣", Symbol = "$" };
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            _context.CurrencyRate.AddRange(new List<CurrencyRate>
        {
            new CurrencyRate { Currency = currency, Rate = 30.5f, UpdatedAt = DateTime.UtcNow.AddHours(-1) },
            new CurrencyRate { Currency = currency, Rate = 31.0f, UpdatedAt = DateTime.UtcNow }
        });
            await _context.SaveChangesAsync();

            // Act
            var result = await _currencyRateService.GetRates("USD");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(31.0f, result.First().Rate);
        }

        [Fact]
        public async Task GetRates_ShouldReturnEmptyList_ForInvalidCurrencyCode()
        {
            // Act
            var result = await _currencyRateService.GetRates("INVALID");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
