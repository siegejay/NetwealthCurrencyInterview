namespace CurrencyExchange.Model
{
    /// <summary>
    /// Currency Exchange Calculator implementation
    /// </summary>
    public class ExchangeBureau : IExchangeBureau
    {
        private readonly IExchangeRateProvider _rateProvider;
        private readonly ICurrencyProvider _currencyProvider;

        public ExchangeBureau(IExchangeRateProvider rateProvider, ICurrencyProvider currencyProvider)
        {
            _rateProvider = rateProvider ?? throw new ArgumentNullException(nameof(rateProvider));
            _currencyProvider = currencyProvider ?? throw new ArgumentNullException(nameof(currencyProvider));
        }

        /// <summary>
        /// Exchange the supplied money value to the requested currency type
        /// </summary>
        /// <param name="value">Money to be exchanged</param>
        /// <param name="toIsoCurrency">Currency to exchange to</param>
        /// <returns>
        ///     Summary record of the exchange attempt
        ///     * If currency types for "from" and "to" are known and the appropriate rate exists, the summary includes the new exchanged value and the rate applied
        ///     * If one or both of the currency types are unknown, the summary has a Null "To" value and a note explaining why exchange could not be calculated
        ///     * If currency types for "from" and "to" are known but the appropriate rate does not exist, the summary has a Null "To" value and a note explaining why exchange could not be calculated
        /// </returns>
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

        /// <summary>
        /// Exchange Summary record for when one of the currency types is not known
        /// </summary>
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

        /// <summary>
        /// Exchange Summary record for when no exchange rate exists for the requested conversion
        /// </summary>
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

        /// <summary>
        /// Exchange Summary record for when the exchange can be successfully completed
        /// </summary>
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
