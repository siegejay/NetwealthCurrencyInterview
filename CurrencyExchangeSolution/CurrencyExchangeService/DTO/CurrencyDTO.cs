using CurrencyExchange.Model;

namespace CurrencyExchange.Service.DTO
{

    //TODO: Look into potential use of Automapper to do the conversion from Model to DTO

    public class CurrencyDTO
    {
        public CurrencyDTO(ICurrency currency)
        {
            Code = currency.CurrencyCode;
            Name = currency.Name;
        }

        public CurrencyDTO(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }

        public string Name { get; }
    }
}
