using CurrencyExchange.Model;

namespace CurrencyExchange.Service.DTO
{
    //TODO: Look into potential use of Automapper to do the conversion from Model to DTO, instead of relying on mapppings in constructors

    /// <summary>
    /// Service DTO representation of Money Data Value
    /// </summary>
    public class MoneyDTO
    {
        public MoneyDTO(IMoney money)
        {
            CurrencyCode = money.CurrencyCode;
            Value = money.Value;
        }

        public MoneyDTO(string currencyCode, decimal? value)
        {
            CurrencyCode = currencyCode;
            Value = value;
        }

        public string CurrencyCode { get; }
        public decimal? Value { get; }

    }
}
