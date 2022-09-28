namespace CurrencyExchange.Model
{
    internal interface IExchangeRate
    {
        string From { get;}
        string To { get; }
        decimal Rate { get; }
    }
}
