using FakeItEasy;

namespace CurrencyExchange.Model.Tests.TestBuilders
{
    internal class ExchangeRateBuilder
    {
        private string _fromCode = "GBP";
        private string _toCode = "USD";
        private decimal _rateValue = 1M;

        public static ExchangeRateBuilder Record()
        {
            return new ExchangeRateBuilder();
        }

        public ExchangeRateBuilder WithFrom(string isoCurrencyCode)
        {
            _fromCode = isoCurrencyCode;
            return this;
        }

        public ExchangeRateBuilder WithTo(string isoCurrencyCode)
        {
            _toCode = isoCurrencyCode;
            return this;
        }

        public ExchangeRateBuilder WithRate(decimal value)
        {
            _rateValue = value;
            return this;
        }

        public IExchangeRate Build()
        {
            var fakeRate = A.Fake<IExchangeRate>();
            A.CallTo(() => fakeRate.From).Returns(_fromCode);
            A.CallTo(() => fakeRate.To).Returns(_toCode);
            A.CallTo(() => fakeRate.Rate).Returns(_rateValue);
            return fakeRate;
        }
    }
}
