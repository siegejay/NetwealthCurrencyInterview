namespace CurrencyExchange.Model
{
    public interface IExchangeRate
    {
        string From { get;}
        string To { get; }
        decimal Rate { get; }
    }
}
