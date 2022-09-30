namespace CurrencyExchange.Model.Tests.TestBuilders
{
    internal class MoneyBuilder
    {
        private decimal _value = 0M;
        private string _isoCurrencyCode = "GBP";
        
        public static MoneyBuilder Record()
        {
            return new MoneyBuilder();
        }

        public MoneyBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public MoneyBuilder WithCurrencyCode(string isoCode)
        {
            _isoCurrencyCode = isoCode;     
            return this;
        }

        public Money Build()
        {
            return new Money(_value, _isoCurrencyCode);
        }

    }
}
