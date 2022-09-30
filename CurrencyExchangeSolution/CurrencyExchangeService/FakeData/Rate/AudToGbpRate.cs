using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class AudToGbpRate : IExchangeRate
    {
        public string From => "AUD";
        public string To => "GBP";
        public decimal Rate => 0.58438M;
    }
}
