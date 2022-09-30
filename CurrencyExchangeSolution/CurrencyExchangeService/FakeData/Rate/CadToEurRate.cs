using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CadToEurRate : IExchangeRate
    {
        public string From => "CAD";
        public string To => "EUR";
        public decimal Rate => 0.74687M;
    }
}
