using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;

namespace CoindeskHomework.BuisnessRules.CoinDesk
{
    public interface IBpiResultConvertService
    {
        Task<List<CurrencyInfoViewModel>> Convert(BpiResult bpiResult);
    }
}