using CoindeskHomework.BuisnessRules.CoinDesk;
using CoindeskHomework.BuisnessRules.Common;
using CoindeskHomework.BuisnessRules.CurrencyRule;
using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;
using CoindeskHomework.Data;
using CoindeskHomework.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));


builder.Services.AddHttpClient<CoinDeskService>();
builder.Services.AddScoped<ICoinDeskService, FakeCoinDeskService>();

builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();
builder.Services.AddScoped<ICoinDeskImportService, CoinDeskImportService>();
builder.Services.AddScoped<IBpiResultConvertService, BpiResultConvertService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
