using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class GbpToCnyRate : IExchangeRate
    {
        public string From => "GBP";
        public string To => "CNY";
        public decimal Rate => 7.89085M;
    }
}
