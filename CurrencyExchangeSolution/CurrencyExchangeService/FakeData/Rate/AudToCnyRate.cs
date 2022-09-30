using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class AudToCnyRate : IExchangeRate
    {
        public string From => "AUD";
        public string To => "CNY";
        public decimal Rate => 0.8891M;
    }
}
