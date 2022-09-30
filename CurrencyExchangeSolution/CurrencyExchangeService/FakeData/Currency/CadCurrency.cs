using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class CadCurrency : ICurrency
    {
        public string CurrencyCode => "CAD";

        public string Name => "Canadian Dollar";
    }
}
