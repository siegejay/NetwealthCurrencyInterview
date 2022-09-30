using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class GbpToAudRate : IExchangeRate
    {
        public string From => "GBP";
        public string To => "AUD";
        public decimal Rate => 1.70999M;
    }
}
