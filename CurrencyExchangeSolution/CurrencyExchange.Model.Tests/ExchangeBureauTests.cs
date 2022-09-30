using Castle.Core.Internal;

namespace CurrencyExchange.Model.Tests
{
    [TestClass]
    public class ExchangeBureauTests
    {
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

        [TestMethod]
        public void ExchangeBureau_Exchange_ForUnknownFromCurrencyType_ReturnsInvalidCurrencyExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================
            var fromCurrencyCode = "AAA";
            var fromValue = 1000M;
            var fromMoney = MoneyBuilder.Record().WithValue(fromValue).WithCurrencyCode(fromCurrencyCode).Build();
            var toCurrencyCode = "BBB";

            var exchangeRate = 1.23456M;

            // Create Fake Currency Provider only returns a Currency Record for the "To" Currency Code 
            var fakeCurrencyProvider = A.Fake<ICurrencyProvider>();
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>.That.Matches(c => c.Equals(toCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(CurrencyRecordBuilder.Record().WithCode(toCurrencyCode).WithName($"Currency: {toCurrencyCode}").Build()
            );
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>.That.Matches(c => !c.Equals(toCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(null);
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>.That.Matches(c => c.Equals(toCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(true);
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>.That.Matches(c => !c.Equals(toCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(false);

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
            Assert.IsNull(summary.To, "The To property should not be instantiated");
            Assert.IsNull(summary.RateApplied, "The RateApplied should not be instantiated");
            Assert.IsFalse(summary.Notes.IsNullOrEmpty(), "The Notes should be populated");
            Assert.IsTrue(summary.Notes.Contains($"'{fromCurrencyCode}'"), "The Notes should include the Unknown Currency Code");
        }

        [TestMethod]
        public void ExchangeBureau_Exchange_ForUnknownToCurrencyType_ReturnsInvalidCurrencyExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================
            var fromCurrencyCode = "AAA";
            var fromValue = 1000M;
            var fromMoney = MoneyBuilder.Record().WithValue(fromValue).WithCurrencyCode(fromCurrencyCode).Build();
            var toCurrencyCode = "BBB";

            var exchangeRate = 1.23456M;

            // Create Fake Currency Provider only returns a Currency Record for the "From" Currency Code 
            var fakeCurrencyProvider = A.Fake<ICurrencyProvider>();
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>.That.Matches(c => c.Equals(fromCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(CurrencyRecordBuilder.Record().WithCode(fromCurrencyCode).WithName($"Currency: {fromCurrencyCode}").Build()
            );
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>.That.Matches(c => !c.Equals(fromCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(null);
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>.That.Matches(c => c.Equals(fromCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(true);
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>.That.Matches(c => !c.Equals(fromCurrencyCode, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(false);

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
            Assert.IsNull(summary.To, "The To property should not be instantiated");
            Assert.IsNull(summary.RateApplied, "The RateApplied should not be instantiated");
            Assert.IsFalse(summary.Notes.IsNullOrEmpty(), "The Notes should be populated");
            Assert.IsTrue(summary.Notes.Contains($"'{toCurrencyCode}'"), "The Notes should include the Unknown Currency Code");
        }

        [TestMethod]
        public void ExchangeBureau_Exchange_ForMissingExchangeRate_ReturnsUnsupportedExchangeSummaryDetails()
        {
            // Arrange
            // ===============================================================
            var fromCurrencyCode = "AAA";
            var fromValue = 1000M;
            var fromMoney = MoneyBuilder.Record().WithValue(fromValue).WithCurrencyCode(fromCurrencyCode).Build();
            var toCurrencyCode = "BBB";

            // Create Fake Currency Provider that always returns a Currency Record with the requested code 
            var fakeCurrencyProvider = A.Fake<ICurrencyProvider>();
            A.CallTo(() => fakeCurrencyProvider.Lookup(A<string>._)).ReturnsLazily((string code) =>
            {
                return CurrencyRecordBuilder.Record().WithCode(code).WithName($"Currency: {code}").Build();
            });
            A.CallTo(() => fakeCurrencyProvider.Exists(A<string>._)).Returns(true);

            // Create Fake Exchange Rate Provider that always returns null
            var fakeRateProvider = A.Fake<IExchangeRateProvider>();
            A.CallTo(() => fakeRateProvider.Lookup(A<ICurrency>._, A<ICurrency>._)).Returns(null);

            var bureau = new ExchangeBureau(fakeRateProvider, fakeCurrencyProvider);

            // Act
            // ===============================================================
            var summary = bureau.Exchange(fromMoney, toCurrencyCode);

            // Assert
            // ===============================================================
            Assert.IsNotNull(summary);
            Assert.IsNotNull(summary.From);
            Assert.AreEqual(fromMoney, summary.From);
            Assert.IsNull(summary.To, "The To property should not be instantiated");
            Assert.IsNull(summary.RateApplied, "The RateApplied should not be instantiated");
            Assert.IsFalse(summary.Notes.IsNullOrEmpty(), "The Notes should be populated");
            Assert.IsTrue(summary.Notes.Contains($"'{fromCurrencyCode}'") && summary.Notes.Contains($"'{toCurrencyCode}'"), 
                "The Notes should include both Currency Codes for the missing exchange rate");
        }

    }
}