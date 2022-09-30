using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class UsdToCnyRate : IExchangeRate
    {
        public string From => "USD";
        public string To => "CNY";
        public decimal Rate => 7.12919M;
    }
}
