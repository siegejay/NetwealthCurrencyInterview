using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CnyToAudRate : IExchangeRate
    {
        public string From => "CNY";
        public string To => "AUD";
        public decimal Rate => 0.2168M;
    }
}
