using ExchangeBureau;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyExchange.Site.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IList<CurrencyDTO> currenciesSupported, CurrencyExchangeInputModel exchangeForm, ExchangeCardDTO? exchangeResult = null)
        {
            CurrenciesList = currenciesSupported;
            ExchangeForm = exchangeForm;
            ExchangeResult = exchangeResult;
        }

        public IList<CurrencyDTO> CurrenciesList { get;  }

        public CurrencyExchangeInputModel ExchangeForm { get; }

        public List<SelectListItem> MakeExchangeFromSelectList() 
        {
                var listItems = new List<SelectListItem>();
                foreach(var item in CurrenciesList)
                {
                    listItems.Add(new SelectListItem($"{item.Name} - {item.Code}", 
                        item.Code, 
                        ExchangeForm.ExchangeFromCode != null && item.Code.Equals(ExchangeForm.ExchangeFromCode)));
                }
                return listItems;
        }

        public List<SelectListItem> MakeExchangeToSelectList()
        {
            var listItems = new List<SelectListItem>();
            foreach (var item in CurrenciesList)
            {
                listItems.Add(new SelectListItem($"{item.Name} - {item.Code}", 
                    item.Code, 
                    ExchangeForm.ExchangeToCode != null && item.Code.Equals(ExchangeForm.ExchangeToCode)));
            }
            return listItems;
        }

        public ExchangeCardDTO? ExchangeResult { get; }
    }
}
