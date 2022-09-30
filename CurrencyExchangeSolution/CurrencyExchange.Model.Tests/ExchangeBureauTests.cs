namespace CurrencyExchange.Model.Tests
{
    [TestClass]
    public class ExchangeBureauTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void ExchangeBureau()
        {
            var _ = new ExchangeBureau(null, A.Fake<ICurrencyProvider>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void ExchangeBureau_New_NoCurrencyProvider()
        {
            var _ = new ExchangeBureau(A.Fake<IExchangeRateProvider>(), null);
        }

        [TestMethod]
        public void ExchangeBureau_Exchange_ForKnownTypesWithExchangeRate_ReturnsExpectedExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================
            var fromCurrencyCode = "AAA";
            var fromValue = 1000M;
            var fromMoney = MoneyBuilder.Record().WithValue(fromValue).WithCurrencyCode(fromCurrencyCode).Build();
            var toCurrencyCode = "BBB";

            var exchangeRate = 1.23456M;

            var expectedToValue = fromValue * exchangeRate;
            var expectedToMoneyValue = MoneyBuilder.Record().WithCurrencyCode(toCurrencyCode).WithValue(expectedToValue).Build();

            // Create Fake Currency Provider that always returns a Currency Record with the requested code 
            var fakeCurrencyProvider = A.Fake<ICurrencyProvider>();
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>._)).ReturnsLazily((string code) =>
            {
                return CurrencyRecordBuilder.Record().WithCode(code).WithName($"Currency: {code}").Build();
            });
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>._)).Returns(true);

            // Create Fake Exchange Rate Provider that always returns an Exchange Rate with the predefined Rate Value
            var fakeRateProvider = A.Fake<IExchangeRateProvider>();
            A.CallTo(() => fakeRateProvider.Lookup(A<ICurrency>._, A<ICurrency>._)).Returns(
                   ExchangeRateBuilder.Record().WithRate(exchangeRate).Build()
                );

            var bureau = new ExchangeBureau(fakeRateProvider, fakeCurrencyProvider);

            // Act
            // ===============================================================
            var summary = bureau.Exchange(fromMoney, toCurrencyCode);

            // Assert
            // ===============================================================
            Assert.IsNotNull(summary);
            Assert.IsNotNull(summary.From);
            Assert.AreEqual(fromMoney, summary.From);
            Assert.IsNotNull(summary.To);
            Assert.AreEqual(expectedToMoneyValue, summary.To);
            Assert.AreEqual(toCurrencyCode, summary.To.CurrencyCode, true);
            Assert.AreEqual(exchangeRate, summary.RateApplied);
        }

        // todo: Complete the following tests 

        [TestMethod]
        public void ExchangeBureau_Exchange_ForUnknownFromCurrencyType_ReturnsInvalidCurrencyExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================

            // Act
            // ===============================================================

            // Assert
            // ===============================================================

        }

        [TestMethod]
        public void ExchangeBureau_Exchange_ForUnknownToCurrencyType_ReturnsInvalidCurrencyExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================

            // Act
            // ===============================================================

            // Assert
            // ===============================================================

        }

        [TestMethod]
        public void ExchangeBureau_Exchange_ForMissingExchangeRate_ReturnsUnsupportedExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================

            // Act
            // ===============================================================

            // Assert
            // ===============================================================

        }

    }
}