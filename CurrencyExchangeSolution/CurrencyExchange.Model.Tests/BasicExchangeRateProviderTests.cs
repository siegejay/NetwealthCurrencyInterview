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

        [TestMethod]
        public void BasicExchangeRateProvider_Lookup_ForRegisteredRate_ReturnsCorrectRateRecord()
        {
            // Arrange
            // ===========================================
            var lookupFromCurrency = CurrencyRecordBuilder.Record().WithCode("Y").Build();
            var lookupToCurrency = CurrencyRecordBuilder.Record().WithCode("Z").Build();
            const decimal expectedRateValue = 0.7221M;
            var expectedRecord = ExchangeRateBuilder.Record()
                .WithFrom(lookupFromCurrency.CurrencyCode)
                .WithTo(lookupToCurrency.CurrencyCode)
                .WithRate(expectedRateValue)
                .Build();

            var registeredRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("A").WithTo("B").WithRate(1.0M).Build(),
                expectedRecord,
                ExchangeRateBuilder.Record().WithFrom("B").WithTo("A").WithRate(0.9M).Build()
            };
            var provider = new BasicExchangeRateProvider(registeredRateRecords.ToArray());

            // Act
            // ===========================================
            var rateRecord = provider.Lookup(lookupFromCurrency, lookupToCurrency);

            // Assert
            // ===========================================
            Assert.IsNotNull(rateRecord);
            Assert.AreEqual(lookupFromCurrency.CurrencyCode, rateRecord.From, true);
            Assert.AreEqual(lookupToCurrency.CurrencyCode, rateRecord.To, true);
            Assert.AreEqual(expectedRateValue, rateRecord.Rate);
        }

        [TestMethod]
        public void BasicExchangeRateProvider_LookupAndExists_ForRegisteredRate_CheckLookupIsCaseInsensitive()
        {
            // Arrange
            // ===========================================
            var lookupFromCurrency = CurrencyRecordBuilder.Record().WithCode("yYy").Build();
            var lookupToCurrency = CurrencyRecordBuilder.Record().WithCode("zZz").Build();
            const decimal expectedRateValue = 0.7221M;
            var expectedRecord = ExchangeRateBuilder.Record()
                .WithFrom(lookupFromCurrency.CurrencyCode.ToUpper())
                .WithTo(lookupToCurrency.CurrencyCode.ToUpper())
                .WithRate(expectedRateValue)
                .Build();

            var registeredRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("A").WithTo("B").WithRate(1.0M).Build(),
                expectedRecord,
                ExchangeRateBuilder.Record().WithFrom("B").WithTo("A").WithRate(0.9M).Build()
            };
            var provider = new BasicExchangeRateProvider(registeredRateRecords.ToArray());

            // Act
            // ===========================================
            var rateRecord = provider.Lookup(lookupFromCurrency, lookupToCurrency);
            var rateExists = provider.Exists(lookupFromCurrency, lookupToCurrency);

            // Assert
            // ===========================================
            // Lookup Method Result Checks
            Assert.IsNotNull(rateRecord);
            Assert.AreEqual(lookupFromCurrency.CurrencyCode, rateRecord.From, true);
            Assert.AreEqual(lookupToCurrency.CurrencyCode, rateRecord.To, true);
            Assert.AreEqual(expectedRateValue, rateRecord.Rate);
            // Exists Method Result Check
            Assert.IsTrue(rateExists);
        }

        [TestMethod]
        public void BasicExchangeRateProvider_Lookup_ForUnregisteredRate_ReturnsNull()
        {
            // Arrange
            // ===========================================
            var lookupFromCurrency = CurrencyRecordBuilder.Record().WithCode("Y").Build();
            var lookupToCurrency = CurrencyRecordBuilder.Record().WithCode("Z").Build();


            var registeredRateRecords = new List<IExchangeRate>
            {
                ExchangeRateBuilder.Record().WithFrom("A").WithTo("B").WithRate(1.0M).Build(),
                ExchangeRateBuilder.Record().WithFrom("B").WithTo("A").WithRate(0.9M).Build()
            };
            var provider = new BasicExchangeRateProvider(registeredRateRecords.ToArray());

            // Act
            // ===========================================
            var rateRecord = provider.Lookup(lookupFromCurrency, lookupToCurrency);

            // Assert
            // ===========================================
            Assert.IsNull(rateRecord);
        }

    }
}
