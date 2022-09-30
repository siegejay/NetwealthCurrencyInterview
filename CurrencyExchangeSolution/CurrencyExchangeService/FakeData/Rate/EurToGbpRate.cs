using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class EurToGbpRate : IExchangeRate
    {
        public string From => "EUR";
        public string To => "GBP";
        public decimal Rate => 0.88119M;
    }
}
