namespace CurrencyExchange.Model.Tests.TestBuilders
{
    internal class CurrencyRecordBuilder
    {
        private string _name = "Currency Name Here";
        private string _code = "AAA";

        public static CurrencyRecordBuilder Record()
        {
            return new CurrencyRecordBuilder();
        } 

        public CurrencyRecordBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CurrencyRecordBuilder WithCode(string code)
        {
            _code = code;
            return this;
        }

        public ICurrency Build()
        {
            var fakeRecord = A.Fake<ICurrency>();
            A.CallTo(() => fakeRecord.Name).Returns(_name);
            A.CallTo(() => fakeRecord.CurrencyCode).Returns(_code);
            return fakeRecord;
        }

    }
}
