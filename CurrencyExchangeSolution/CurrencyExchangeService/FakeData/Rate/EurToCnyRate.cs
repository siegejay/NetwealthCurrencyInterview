using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class EurToCnyRate : IExchangeRate
    {
        public string From => "EUR";
        public string To => "CNY";
        public decimal Rate => 6.94901M;
    }
}
