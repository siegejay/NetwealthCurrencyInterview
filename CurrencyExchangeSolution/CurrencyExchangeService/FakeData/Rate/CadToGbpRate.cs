using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class CadToGbpRate : IExchangeRate
    {
        public string From => "CAD";
        public string To => "GBP";
        public decimal Rate => 0.65806M;
    }
}
