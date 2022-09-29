using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class UsdToGbpRate : IExchangeRate
    {
        public string From => "USD";

        public string To => "GBP";
        public decimal Rate => 1.0844M;
    }
}
