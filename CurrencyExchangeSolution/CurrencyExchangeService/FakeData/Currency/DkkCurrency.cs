using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class DkkCurrency : ICurrency
    {
        public string CurrencyCode => "DKK";

        public string Name => "Danish Krone";
    }
}
