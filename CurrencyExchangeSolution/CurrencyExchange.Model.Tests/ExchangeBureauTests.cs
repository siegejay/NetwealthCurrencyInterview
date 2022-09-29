using FakeItEasy;

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


    }
}