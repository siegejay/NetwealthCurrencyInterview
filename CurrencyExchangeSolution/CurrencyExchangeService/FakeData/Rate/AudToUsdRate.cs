using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Rate
{
    public class AudToUsdRate : IExchangeRate
    {
        public string From => "AUD";

        public string To => "USD";
        public decimal Rate => 0.6477M;
    }
}
