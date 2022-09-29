using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class GbpCurrency : ICurrency
    {
        public string CurrencyCode => "GBP";

        public string Name => "Great Britain Pounds";
    }
}
