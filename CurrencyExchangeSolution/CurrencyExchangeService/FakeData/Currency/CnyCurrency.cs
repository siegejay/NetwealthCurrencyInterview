using CurrencyExchange.Model;

namespace CurrencyExchange.Service.FakeData.Currency
{
    public class CnyCurrency : ICurrency
    {
        public string CurrencyCode => "CNY";

        public string Name => "China Yuan Renminbi";
    }
}
