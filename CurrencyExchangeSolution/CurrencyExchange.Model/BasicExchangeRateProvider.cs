using System.Linq;

namespace CurrencyExchange.Model
{
    public class BasicExchangeRateProvider : IExchangeRateProvider
    {
        private readonly List<IExchangeRate> _rates;

        public BasicExchangeRateProvider(IExchangeRate[] registeredRates)
        {
            // Ensure we fail early if Registered Exchange Rates include duplicates (based on From and To currency codes)
            if (registeredRates == null) registeredRates = Array.Empty<IExchangeRate>();
            if (registeredRates
                    .GroupBy(r => $"{r.From}-{r.To}", StringComparer.InvariantCultureIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .ToList().Count > 0) 
                throw new ArgumentException("Duplicate exchange records have been registered", nameof(registeredRates));

            _rates = registeredRates.ToList();
        }

        public bool Exists(ICurrency from, ICurrency to)
        {
            return Lookup(from, to) != null;
        }

        public IExchangeRate? Lookup(ICurrency from, ICurrency to)
        {
            if (from.CurrencyCode.Equals(to.CurrencyCode)) return new OneToOneExchangeRate(from, to);
            
            return _rates.SingleOrDefault(r =>
                r.From.Equals(from.CurrencyCode, StringComparison.InvariantCultureIgnoreCase)
                && r.To.Equals(to.CurrencyCode, StringComparison.InvariantCultureIgnoreCase)
            );
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
