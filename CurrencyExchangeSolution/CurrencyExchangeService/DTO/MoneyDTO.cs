using CurrencyExchange.Model;

namespace CurrencyExchange.Service.DTO
{
    public class MoneyDTO
    {
        public MoneyDTO(IMoney money)
        {
            CurrencyCode = money.CurrencyCode;
            Value = money.Value;
        }

        public MoneyDTO(string currencyCode, decimal value)
        {
            CurrencyCode = currencyCode;
            Value = value;
        }

        public string CurrencyCode { get; }
        public decimal Value { get; }

    }
}
