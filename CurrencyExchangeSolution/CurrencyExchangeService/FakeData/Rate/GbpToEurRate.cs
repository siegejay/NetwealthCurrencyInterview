using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class GbpToEurRate : IExchangeRate
    {
        public string From => "GBP";
        public string To => "EUR";
        public decimal Rate => 1.13491M;
    }
}
