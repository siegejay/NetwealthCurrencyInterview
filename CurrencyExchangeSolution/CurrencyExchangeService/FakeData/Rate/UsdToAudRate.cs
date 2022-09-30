using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class UsdToAudRate : IExchangeRate
    {
        public string From => "USD";
        public string To => "AUD";
        public decimal Rate => 1.54534M;
    }
}
