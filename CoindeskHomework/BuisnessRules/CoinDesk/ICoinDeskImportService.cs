using CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk;

namespace CoindeskHomework.BuisnessRules.CoinDesk
{
    public interface ICoinDeskImportService
    {
        Task Import(BpiResult bpiResult);
    }
}