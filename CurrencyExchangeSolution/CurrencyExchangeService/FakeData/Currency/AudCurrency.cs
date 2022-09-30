using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class AudCurrency : ICurrency
    {
        public string CurrencyCode => "AUD";

        public string Name => "Australian Dollar";
    }
}
