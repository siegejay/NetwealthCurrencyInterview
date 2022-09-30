using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class AudToEurRate : IExchangeRate
    {
        public string From => "AUD";
        public string To => "EUR";
        public decimal Rate => 0.66331M;
    }
}
