namespace CurrencyExchange.Model.Tests
{
    [TestClass]
    public class SameFromToExchangeRateProviderDecoratorTests
    {
        [TestMethod]
        public void SameFromToExchangeRateProviderDecorator_LookUp_ForMatchingCurrencies_ReturnsAOneToOneRateRecord()
        {
            // Arrange
            // ==================================================
            var decoratedBaseProvider = A.Fake<IExchangeRateProvider>();
            A.CallTo(() => decoratedBaseProvider.Lookup(A<ICurrency>._, A<ICurrency>._)).Returns(null);
            
            var provider = new SameFromToExchangeRateProviderDecorator(decoratedBaseProvider); // Base acts as if it does not have this record

            const string lookupFromAndToCode = "AAA";
            var lookupCurrencyRecord = CurrencyRecordBuilder.Record()
                .WithCode(lookupFromAndToCode)
                .WithName("Test Currency")
                .Build();

            // Act
            // ==================================================
            var rateRecord = provider.Lookup(lookupCurrencyRecord, lookupCurrencyRecord);

            // Assert
            // ==================================================
            Assert.IsNotNull(rateRecord);
            Assert.AreEqual(lookupFromAndToCode, rateRecord.From, true);
            Assert.AreEqual(lookupFromAndToCode, rateRecord.To, true);
            Assert.AreEqual(1M, rateRecord.Rate);
        }

        [TestMethod]
        public void SameFromToExchangeRateProviderDecorator_LookUp_ForMatchingCurrencies_CheckLookupIsCaseInsensitive()
        {
            // Arrange
            // ==================================================
            var decoratedBaseProvider = A.Fake<IExchangeRateProvider>();
            A.CallTo(() => decoratedBaseProvider.Lookup(A<ICurrency>._, A<ICurrency>._)).Returns(null);

            var provider = new SameFromToExchangeRateProviderDecorator(decoratedBaseProvider); // Base acts as if it does not have this record

            const string lookupFromAndToCode = "AAA";
            var upperCaseLookupCurrencyRecord = CurrencyRecordBuilder.Record()
                .WithCode(lookupFromAndToCode)
                .WithName("Test Currency With Upper Case Code")
                .Build();
            var lowerCaseLookupCurrencyRecord = CurrencyRecordBuilder.Record()
                .WithCode(lookupFromAndToCode.ToLower())
                .WithName("Test Currency With Lower Case Code")
                .Build();

            // Act
            // ==================================================
            var rateRecord = provider.Lookup(upperCaseLookupCurrencyRecord, lowerCaseLookupCurrencyRecord);

            // Assert
            // ==================================================
            Assert.IsNotNull(rateRecord);
            Assert.AreEqual(lookupFromAndToCode, rateRecord.From, true);
            Assert.AreEqual(lookupFromAndToCode, rateRecord.To, true);
            Assert.AreEqual(1M, rateRecord.Rate);
        }

        [TestMethod]
        public void SameFromToExchangeRateProviderDecorator_Exists_ForMatchingCurrencies_ReturnsTrue()
        {
            // Arrange
            // ==================================================
            var decoratedBaseProvider = A.Fake<IExchangeRateProvider>();
            A.CallTo(() => decoratedBaseProvider.Lookup(A<ICurrency>._, A<ICurrency>._)).Returns(null); // Base acts as if it does not have this record

            var provider = new SameFromToExchangeRateProviderDecorator(decoratedBaseProvider);

            const string lookupFromAndToCode = "AAA";
            var lookupCurrencyRecord = CurrencyRecordBuilder.Record()
                .WithCode(lookupFromAndToCode)
                .WithName("Test Currency")
                .Build();

            // Act
            // ==================================================
            var rateExists = provider.Exists(lookupCurrencyRecord, lookupCurrencyRecord);

            // Assert
            // ==================================================
            Assert.IsNotNull(rateExists);
            Assert.IsTrue(rateExists);
        }

    }
}
