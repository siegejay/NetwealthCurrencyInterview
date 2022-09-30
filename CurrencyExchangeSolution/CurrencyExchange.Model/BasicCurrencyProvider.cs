namespace CurrencyExchange.Model
{
    /// <summary>
    /// Simple Currency Provider - Designed to be fed a set of predefined currencies that the exchange calculator supports on construction 
    /// </summary>
    public class BasicCurrencyProvider: ICurrencyProvider
    {
        private readonly List<ICurrency> _currencies;

        /// <summary>
        /// Construct Provider
        /// </summary>
        /// <param name="registeredCurrencies">Set of predefined currency records</param>
        /// <exception cref="ArgumentException">Exceptions if more than one record has the same code</exception>
        public BasicCurrencyProvider(ICurrency[] registeredCurrencies)
        {
            // Ensure we fail early if any duplicate currency codes registered
            if (registeredCurrencies == null) registeredCurrencies = Array.Empty<ICurrency>();
            if (registeredCurrencies
                    .GroupBy(r => r.CurrencyCode, StringComparer.InvariantCultureIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .ToList().Count > 0)
                throw new ArgumentException("Duplicate currency records have been registered", nameof(registeredCurrencies));

            _currencies = registeredCurrencies == null? new List<ICurrency>(): registeredCurrencies.ToList();            
        }

        public IList<ICurrency> All()
        {
            return _currencies;
        }

        public bool Exists(string isoCurrencyCode)
        {
            return Lookup(isoCurrencyCode) != null;
        }

        public ICurrency? Lookup(string isoCurrencyCode)
        {
            return _currencies.SingleOrDefault(c => c.CurrencyCode.Equals(isoCurrencyCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
