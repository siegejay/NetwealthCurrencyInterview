namespace CurrencyExchange.Model
{
    public interface IExchangeSummary
    {
        IMoney From { get; }
        IMoney? To { get; }
        decimal? RateApplied { get; }
        string Notes { get; }
    }
}
