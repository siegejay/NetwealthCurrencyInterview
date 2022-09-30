using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CadToUsdRate : IExchangeRate
    {
        public string From => "CAD";
        public string To => "USD";
        public decimal Rate => 0.72842M;
    }
}
