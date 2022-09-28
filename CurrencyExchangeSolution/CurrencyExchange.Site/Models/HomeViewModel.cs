using ExchangeEngine;

namespace CurrencyExchange.Site.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IList<CurrencyDTO> currenciesSupported)
        {
            Currencies = currenciesSupported;
        }

        public IList<CurrencyDTO> Currencies { get; set; }

        public string? ExchangeFromCode { get; set; }
        public string? ExchangeToCode { get; set; }
        public decimal? FromValue { get; set; }
        public decimal? ToValue { get; set; }
    }
}
