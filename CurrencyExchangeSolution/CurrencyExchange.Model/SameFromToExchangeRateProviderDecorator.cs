namespace CurrencyExchange.Model
{

    /// <summary>
    /// Decorator to entend the RateProvider behaviour to automatically ensure a one to one rate is returned 
    /// when From to To currencies are the same
    /// </summary>
    public class SameFromToExchangeRateProviderDecorator : IExchangeRateProvider
    {
        private readonly IExchangeRateProvider _baseProvider;

        public SameFromToExchangeRateProviderDecorator(IExchangeRateProvider providerToDecorate)
        {
            _baseProvider = providerToDecorate;
        }

        public bool Exists(ICurrency from, ICurrency to)
        {
            return Lookup(from, to) != null;
        }

        public IExchangeRate? Lookup(ICurrency from, ICurrency to)
        {
            if (from.CurrencyCode.Equals(to.CurrencyCode, StringComparison.InvariantCultureIgnoreCase)) return new OneToOneExchangeRate(from, to);
            return _baseProvider.Lookup(from, to);
        }

        private class OneToOneExchangeRate : IExchangeRate
        {
            public OneToOneExchangeRate(ICurrency from, ICurrency to)
            {
                From = from.CurrencyCode;
                To = to.CurrencyCode;
            }

            public string From { get; }

            public string To { get; }

            public decimal Rate => 1M;
        }
    }
}
