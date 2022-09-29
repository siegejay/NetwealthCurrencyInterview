using CurrencyExchange.Model;

namespace CurrencyExchange.Service.DTO
{
    public class ExchangeCardDTO
    {
        public ExchangeCardDTO(IExchangeSummary summary)
        {
            From = new MoneyDTO(summary.From);
            To = summary.To != null? new MoneyDTO(summary.To): null;
            RateApplied = summary.RateApplied;
            Notes = summary.Notes;
        }

        public MoneyDTO From { get; }
        public MoneyDTO? To { get; }
        public decimal? RateApplied { get; }
        public string Notes { get; } = string.Empty;
    }
}
