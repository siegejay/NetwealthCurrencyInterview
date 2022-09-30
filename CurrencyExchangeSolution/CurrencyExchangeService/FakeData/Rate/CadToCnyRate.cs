using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CadToCnyRate : IExchangeRate
    {
        public string From => "CAD";
        public string To => "CNY";
        public decimal Rate => 5.1921M;
    }
}
