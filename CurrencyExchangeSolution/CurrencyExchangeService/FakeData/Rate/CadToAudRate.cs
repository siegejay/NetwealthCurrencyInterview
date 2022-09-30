using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CadToAudRate : IExchangeRate
    {
        public string From => "CAD";
        public string To => "AUD";
        public decimal Rate => 1.12402M;
    }
}
