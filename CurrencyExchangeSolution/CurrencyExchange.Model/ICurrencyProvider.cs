namespace CurrencyExchange.Model
{
    public interface ICurrencyProvider
    {
        IList<ICurrency> All();
        ICurrency? Lookup(string isoCurrencyCode);
        bool Exists(string isoCurrencyCode);
    }
}
