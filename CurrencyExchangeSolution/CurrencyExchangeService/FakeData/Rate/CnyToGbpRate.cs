using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CnyToGbpRate : IExchangeRate
    {
        public string From => "CNY";
        public string To => "GBP";
        public decimal Rate => 0.12691M;
    }
}
