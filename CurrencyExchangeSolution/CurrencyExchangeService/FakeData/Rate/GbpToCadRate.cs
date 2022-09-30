using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class GbpToCadRate : IExchangeRate
    {
        public string From => "GBP";
        public string To => "CAD";
        public decimal Rate => 1.52031M;
    }
}
