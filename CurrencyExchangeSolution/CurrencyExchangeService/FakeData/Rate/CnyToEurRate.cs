using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CnyToEurRate : IExchangeRate
    {
        public string From => "CNY";
        public string To => "EUR";
        public decimal Rate => 0.1439M;
    }
}
