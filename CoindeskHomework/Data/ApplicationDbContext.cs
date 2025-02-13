using CoindeskHomework.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoindeskHomework.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<CurrencyRate> CurrencyRate { get; set; }
    }
}
