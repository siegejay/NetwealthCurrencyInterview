namespace CurrencyExchange.Model
{
    public interface IMoneyExchange
    {
        IMoney Exchange(IMoney value, ICurrency to);
    }
}
