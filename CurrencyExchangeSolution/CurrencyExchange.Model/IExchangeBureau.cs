namespace CurrencyExchange.Model
{
    public interface IExchangeBureau
    {
        IExchangeSummary Exchange(IMoney value, string toIsoCurrency);
    }
}
