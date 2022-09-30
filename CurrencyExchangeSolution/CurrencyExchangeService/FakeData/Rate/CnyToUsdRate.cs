using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CnyToUsdRate : IExchangeRate
    {
        public string From => "CNY";
        public string To => "USD";
        public decimal Rate => 0.14027M;
    }
}
