using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class UsdCurrency : ICurrency
    {
        public string CurrencyCode => "USD";

        public string Name => "United States Dollars";
    }
}
