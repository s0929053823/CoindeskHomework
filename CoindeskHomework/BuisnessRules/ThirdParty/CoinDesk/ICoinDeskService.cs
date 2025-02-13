
namespace CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk
{
    public interface ICoinDeskService
    {
        Task<BpiResult?> GetCurrencyInfoAsync();
    }
}