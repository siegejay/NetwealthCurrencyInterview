namespace CurrencyExchange.Model
{
    public class BasicCurrencyProvider: ICurrencyProvider
    {
        private readonly List<ICurrency> _currencies;
        public BasicCurrencyProvider(ICurrency[] registeredCurrencies)
        {
            // Ensure we fail early if any duplicate currency codes registered
            if (registeredCurrencies == null) registeredCurrencies = Array.Empty<ICurrency>();
            if (registeredCurrencies
                    .GroupBy(r => r.CurrencyCode, StringComparer.InvariantCultureIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .ToList().Count > 0)
                throw new ArgumentException("Duplicate curerncy records have been registered", nameof(registeredCurrencies));

            _currencies = registeredCurrencies == null? new List<ICurrency>(): registeredCurrencies.ToList();            
        }

        public IList<ICurrency> All()
        {
            return _currencies;
        }

        public bool Exists(string isoCurrencyCode)
        {
            return _currencies.Exists(key => key.CurrencyCode.Equals(isoCurrencyCode, StringComparison.InvariantCultureIgnoreCase));
        }

        public ICurrency? Lookup(string isoCurrencyCode)
        {
            return _currencies.SingleOrDefault(c => c.CurrencyCode.Equals(isoCurrencyCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
