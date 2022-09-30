using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class UsdToEurRate : IExchangeRate
    {
        public string From => "USD";
        public string To => "EUR";
        public decimal Rate => 1.02537M;
    }
}
