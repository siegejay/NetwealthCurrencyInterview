using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class GbpToUsdRate : IExchangeRate
    {
        public string From => "GBP";
        public string To => "USD";
        public decimal Rate => 1.1074M;
    }
}
