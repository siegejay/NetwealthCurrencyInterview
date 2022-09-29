namespace CurrencyExchange.Model
{
    public class ExchangeBureau : IExchangeBureau
    {
        private readonly IExchangeRateProvider _rateProvider;
        private readonly ICurrencyProvider _currencyProvider;

        public ExchangeBureau(IExchangeRateProvider rateProvider, ICurrencyProvider currencyProvider)
        {
            _rateProvider = rateProvider ?? throw new ArgumentNullException(nameof(rateProvider));
            _currencyProvider = currencyProvider ?? throw new ArgumentNullException(nameof(currencyProvider));
        }

        public IExchangeSummary Exchange(IMoney value, string toIsoCurrency)
        {
            // Check Currencies are registered
            var fromCurrency = _currencyProvider.Lookup(value.CurrencyCode);
            if (fromCurrency == null) return new InvalidCurrencyExchangeSummary(value, value.CurrencyCode);
            var toCurrency = _currencyProvider.Lookup(toIsoCurrency);
            if (toCurrency == null) return new InvalidCurrencyExchangeSummary(value, toIsoCurrency);

            // Check Exchange Rate is available for this exchange
            var rateCard = _rateProvider.Lookup(fromCurrency, toCurrency);
            if (rateCard == null) return new MissingRateExchangeSummary(value, toCurrency.CurrencyCode);

            // Calculate the exchange value
            var exchangeValue = value.Value * rateCard.Rate;

            // TODO: Find out business rules in terms of rounding requirements on the exchanged value

            // Compile a Exchange Summary and return
            return new CompletedExchangeSummary(
                new Money(value.Value, fromCurrency.CurrencyCode), 
                new Money(exchangeValue, toCurrency.CurrencyCode), 
                rateCard.Rate);
        }

        private class InvalidCurrencyExchangeSummary : IExchangeSummary
        {
            private readonly string _unrecognisedCode;

            public InvalidCurrencyExchangeSummary(IMoney from, string unrecognisedCode)
            {
                _unrecognisedCode = unrecognisedCode;
                From = from;
            }

            public IMoney From { get; }
            public IMoney? To => null;
            public decimal? RateApplied => null;
            public string Notes => $"The currency code: '{_unrecognisedCode}' is not registered with this exchange bureau";
        }

        private class MissingRateExchangeSummary : IExchangeSummary
        {
            private readonly string _toCode;

            public MissingRateExchangeSummary(IMoney from, string toCode)
            {
                _toCode = toCode;
                From = from;
            }

            public IMoney From { get; }
            public IMoney? To => null;
            public decimal? RateApplied => null;
            public string Notes => $"This exchange bureau does not currently support exchanges from Currency Code: '{From.CurrencyCode}' to '{_toCode}'";
        }

        private class CompletedExchangeSummary : IExchangeSummary
        {
            public CompletedExchangeSummary(IMoney from, IMoney to, decimal rateApplied)
            {
                From = from;
                To = to;
                RateApplied = rateApplied;
            }

            public IMoney From { get; }
            public IMoney? To { get; }
            public decimal? RateApplied { get; }
            public string Notes => string.Empty;
        }
    }
}
