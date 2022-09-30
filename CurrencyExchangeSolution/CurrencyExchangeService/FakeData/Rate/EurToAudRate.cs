using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class EurToAudRate : IExchangeRate
    {
        public string From => "EUR";
        public string To => "AUD";
        public decimal Rate => 1.50513M;
    }
}
