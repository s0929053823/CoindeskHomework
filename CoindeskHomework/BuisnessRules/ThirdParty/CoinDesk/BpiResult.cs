namespace CoindeskHomework.BuisnessRules.ThirdParty.CoinDesk
{

    public class BpiResult
    {
        public Time time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Dictionary<string, CurrencyInfo> bpi { get; set; }

        public class Time
        {
            public string updated { get; set; }
            public DateTime updatedISO { get; set; }
            public string updateduk { get; set; }
        }

        public class CurrencyInfo
        {
            public string code { get; set; }
            public string symbol { get; set; }
            public string rate { get; set; }
            public string description { get; set; }
            public float rate_float { get; set; }
        }
   
    }

}
