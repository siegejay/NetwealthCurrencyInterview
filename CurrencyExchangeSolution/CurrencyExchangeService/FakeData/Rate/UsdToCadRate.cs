using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class UsdToCadRate : IExchangeRate
    {
        public string From => "USD";
        public string To => "CAD";
        public decimal Rate => 1.37365M;
    }
}
