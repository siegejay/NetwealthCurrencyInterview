using CurrencyExchange.Model.Tests.TestBuilders;

namespace CurrencyExchange.Model.Tests
{
    [TestClass]
    public class BasicExchangeRateProviderTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void BasicExchangeRateProvider_New_DuplicateRateRecords_ThrowException()
        {
            // Arrange
            // ===========================================
            var dupRateRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("AAA").WithTo("BBB").WithRate(1.0M).Build(),
                ExchangeRateBuilder.Record().WithFrom("AAA").WithTo("BBB").WithRate(0.9M).Build()
            };

            // Act
            // ===========================================
            var _ = new BasicExchangeRateProvider(dupRateRateRecords.ToArray());
        }

        [TestMethod]
        public void BasicExchangeRateProvider_Exists_ForRegisteredRate_ReturnsTrue()
        {
            // Arrange
            // ===========================================
            var lookupFromCurrency = CurrencyRecordBuilder.Record().WithCode("Y").Build();
            var lookupToCurrency = CurrencyRecordBuilder.Record().WithCode("Z").Build();

            var registeredRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("A").WithTo("B").WithRate(1.0M).Build(),
                ExchangeRateBuilder.Record().WithFrom(lookupFromCurrency.CurrencyCode).WithTo(lookupToCurrency.CurrencyCode).WithRate(0.9M).Build(),
                ExchangeRateBuilder.Record().WithFrom("B").WithTo("A").WithRate(0.9M).Build()
            };
            var provider = new BasicExchangeRateProvider(registeredRateRecords.ToArray());

            // Act
            // ===========================================
            var rateExists = provider.Exists(lookupFromCurrency, lookupToCurrency);

            // Assert
            // ===========================================
            Assert.IsNotNull(rateExists);
            Assert.IsTrue(rateExists);
        }

        [TestMethod]
        public void BasicExchangeRateProvider_Exists_ForUnregisteredRate_ReturnsFalse()
        {
            // Arrange
            // ===========================================
            var lookupFromCurrency = CurrencyRecordBuilder.Record().WithCode("Y").Build();
            var lookupToCurrency = CurrencyRecordBuilder.Record().WithCode("Z").Build();

            var registeredRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("A").WithTo("B").WithRate(1.0M).Build(),
                ExchangeRateBuilder.Record().WithFrom("B").WithTo("A").WithRate(0.8M).Build()
            };
            var provider = new BasicExchangeRateProvider(registeredRateRecords.ToArray());

            // Act
            // ===========================================
            var rateExists = provider.Exists(lookupFromCurrency, lookupToCurrency);

            // Assert
            // ===========================================
            Assert.IsNotNull(rateExists);
            Assert.IsFalse(rateExists);
        }

    }
}
