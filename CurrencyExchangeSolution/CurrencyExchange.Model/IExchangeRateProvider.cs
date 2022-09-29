namespace CurrencyExchange.Model
{
    public interface IExchangeRateProvider
    {
        IExchangeRate? Lookup(ICurrency from, ICurrency to);
        bool Exists(ICurrency from, ICurrency to);
    }
}
