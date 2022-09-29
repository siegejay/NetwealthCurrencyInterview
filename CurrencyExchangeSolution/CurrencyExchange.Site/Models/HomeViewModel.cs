using ExchangeBureau;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyExchange.Site.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IList<CurrencyDTO> currenciesSupported)
        {
            CurrenciesList = currenciesSupported;
        }

        public IList<CurrencyDTO> CurrenciesList { get;  }

        public List<SelectListItem> MakeExchangeFromSelectList() 
        {
                var listItems = new List<SelectListItem>();
                foreach(var item in CurrenciesList)
                {
                    listItems.Add(new SelectListItem(item.Name, item.Code, ExchangeFromCode != null && item.Code.Equals(ExchangeFromCode)));
                }
                return listItems;
        }

        public string? ExchangeFromCode { get; set; }

        public List<SelectListItem> MakeExchangeToSelectList()
        {
            var listItems = new List<SelectListItem>();
            foreach (var item in CurrenciesList)
            {
                listItems.Add(new SelectListItem(item.Name, item.Code, ExchangeToCode != null && item.Code.Equals(ExchangeToCode)));
            }
            return listItems;
        }

        public string? ExchangeToCode { get; set; }
        public decimal? FromValue { get; set; }
        public decimal? ToValue { get; set; }
    }
}
