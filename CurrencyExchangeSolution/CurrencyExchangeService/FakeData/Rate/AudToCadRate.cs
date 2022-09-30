using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class AudToCadRate : IExchangeRate
    {
        public string From => "AUD";
        public string To => "CAD";
        public decimal Rate => 0.8891M;
    }
}
