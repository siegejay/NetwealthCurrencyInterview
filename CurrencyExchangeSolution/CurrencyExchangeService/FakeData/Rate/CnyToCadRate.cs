using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CnyToCadRate : IExchangeRate
    {
        public string From => "CNY";
        public string To => "CAD";
        public decimal Rate => 0.19275M;
    }
}
