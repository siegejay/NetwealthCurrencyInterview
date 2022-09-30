using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class EurToCadRate : IExchangeRate
    {
        public string From => "EUR";
        public string To => "CAD";
        public decimal Rate => 1.33811M;
    }
}
