using CurrencyExchange.Model;

namespace CurrencyExchange.Service.DTO
{
    //TODO: Look into potential use of Automapper to do the conversion from Model to DTO, instead of relying on mapppings in constructors

    /// <summary>
    /// Service DTO representation of the Exchange Card
    /// </summary>
    public class ExchangeCardDTO
    {
        public ExchangeCardDTO(IExchangeSummary summary)
        {
            From = new MoneyDTO(summary.From);
            To = summary.To != null? new MoneyDTO(summary.To): new MoneyDTO("", null);
            RateApplied = summary.RateApplied;
            Notes = summary.Notes;
        }

        public MoneyDTO From { get; }
        public MoneyDTO To { get; }
        public decimal? RateApplied { get; }
        public string Notes { get; } = string.Empty;
    }
}
