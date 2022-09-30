namespace CurrencyExchange.Site.Models
{
    public class CurrencyExchangeInputModel
    {
        public string? ExchangeFromCode { get; set; }
        public string? ExchangeToCode { get; set; }
        public decimal? FromValue { get; set; }
    }
}
