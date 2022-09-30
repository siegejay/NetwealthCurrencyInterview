using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class EurToUsdRate : IExchangeRate
    {
        public string From => "EUR";
        public string To => "USD";
        public decimal Rate => 0.97464M;
    }
}
