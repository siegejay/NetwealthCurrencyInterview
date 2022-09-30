namespace CurrencyExchange.Model
{
    /// <summary>
    /// Simple Exchange Rate Provider, which is fed a preselected set of rates on construction
    /// </summary>
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
             return _rates.SingleOrDefault(r =>
                r.From.Equals(from.CurrencyCode, StringComparison.InvariantCultureIgnoreCase)
                && r.To.Equals(to.CurrencyCode, StringComparison.InvariantCultureIgnoreCase)
            );
        }
    }
}
