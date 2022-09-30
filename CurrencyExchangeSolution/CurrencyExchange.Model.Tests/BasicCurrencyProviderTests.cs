namespace CurrencyExchange.Model.Tests
{
    [TestClass]
    public class BasicCurrencyProviderTests
    {

        [TestMethod, ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void BasicCurrencyProvider_New_DuplicateCodeRecords()
        {
            // Arrange
            // ===========================================================
            const string duplicateCode = "DUP";
            var currencyRecords = new List<ICurrency>();
            currencyRecords.Add(CurrencyRecordBuilder.Record()
                .WithCode(duplicateCode)
                .WithName("Record A")
                .Build());
            currencyRecords.Add(CurrencyRecordBuilder.Record()
                .WithCode(duplicateCode)
                .WithName("Record B")
                .Build());

            // Act
            // ===========================================================
            var _ = new BasicCurrencyProvider(currencyRecords.ToArray());
        }


        [TestMethod]
        public void BasicCurrencyProvider_All_ReturnCorrectNumberOfRecords()
        {
            // Arrange
            // ============================================================
            const int expectedRecords = 20;
            var preregisteredCurrencyList = MakeCurrencyListUsingNumberCodes(expectedRecords);

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var allRecords = provider.All();

            // Assert
            // ============================================================
            Assert.IsNotNull(allRecords);
            Assert.AreEqual(expectedRecords, allRecords.Count);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Exists_ForRegisteredRecord_ReturnTrue()
        {
            // Arrange
            // ============================================================
            var lookupCode = 12.ToString(); 
            var preregisteredCurrencyList = MakeCurrencyListUsingNumberCodes(20);

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var recordExists = provider.Exists(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNotNull(recordExists);
            Assert.IsTrue(recordExists);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Exists_ForRegisteredRecord_CheckCaseInsensitive()
        {
            // Arrange
            // ============================================================
            var lookupCode = "bBb";
            var preregisteredCurrencyList = new List<ICurrency>()
                { CurrencyRecordBuilder.Record()
                .WithCode(lookupCode.ToUpper()) // Ensure Registered code is in different case to lookup code
                .WithName("Currency with Upper Case Code")
                .Build()
            };

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var recordExists = provider.Exists(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNotNull(recordExists);
            Assert.IsTrue(recordExists);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Exists_ForUnregisteredRecord_ReturnsFalse()
        {
            // Arrange
            // ============================================================
            var lookupCode = "A";
            var preregisteredCurrencyList = MakeCurrencyListUsingNumberCodes(20);

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var recordExists = provider.Exists(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNotNull(recordExists);
            Assert.IsFalse(recordExists);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Lookup_ForRegisteredRecord_ReturnCorrectRecord()
        {
            // Arrange
            // ============================================================
            var lookupCode = 12.ToString();
            var preregisteredCurrencyList = MakeCurrencyListUsingNumberCodes(20);

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var record = provider.Lookup(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNotNull(record);
            Assert.AreEqual(lookupCode, record.CurrencyCode, true);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Lookup_ForUnregisteredRecord_ReturnsNull()
        {
            // Arrange
            // ============================================================
            var lookupCode = "A";
            var preregisteredCurrencyList = MakeCurrencyListUsingNumberCodes(20);

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var record = provider.Lookup(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNull(record);
        }

        [TestMethod]
        public void BasicCurrencyProvider_Lookup_ForRegisteredRecord_CheckCaseInsensitive()
        {
            // Arrange
            // ============================================================
            var lookupCode = "aAa";
            var preregisteredCurrencyList = new List<ICurrency>() 
                { CurrencyRecordBuilder.Record()
                .WithCode(lookupCode.ToUpper()) // Ensure Registered code is in different case to lookup code
                .WithName("Currency with Upper Case Code")
                .Build()
            };

            var provider = new BasicCurrencyProvider(preregisteredCurrencyList.ToArray());

            // Act
            // ============================================================
            var record = provider.Lookup(lookupCode);

            // Assert
            // ============================================================
            Assert.IsNotNull(record);
            Assert.AreEqual(lookupCode, record.CurrencyCode, true);
        }

        private IList<ICurrency> MakeCurrencyListUsingNumberCodes(int recordCount)
        {
            var list = new List<ICurrency>();
            for (var i = 1; i <= recordCount; i++)
            {
                list.Add(CurrencyRecordBuilder.Record()
                    .WithCode(i.ToString())
                    .WithName($"Currency {i}")
                    .Build());
            }
            return list;
        }

    }
}
