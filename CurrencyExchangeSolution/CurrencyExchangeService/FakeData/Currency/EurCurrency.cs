using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class EurCurrency : ICurrency
    {
        public string CurrencyCode => "EUR";

        public string Name => "Euro";
    }
}
