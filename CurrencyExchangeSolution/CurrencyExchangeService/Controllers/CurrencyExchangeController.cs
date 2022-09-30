using CurrencyExchange.Model;
using CurrencyExchange.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Service.Controllers
{
    [ApiController]
    [Route("currency")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyProvider _currencyProvider;
        private readonly IExchangeBureau _bureau;
        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ICurrencyProvider currencyProvider, IExchangeBureau bureau, ILogger<CurrencyExchangeController> logger)
        {
            _currencyProvider = currencyProvider;
            _bureau = bureau;
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IList<CurrencyDTO> GetCurrencyList()
        {
            var currencyList = _currencyProvider.All().ToList().ConvertAll(c => new CurrencyDTO(c));
            return currencyList;
        }

        [Route("exchange")]
        [HttpGet()]
        public ExchangeCardDTO Exchange(string from, decimal value, string to)
        {
            var exchangeResult = _bureau.Exchange(new Money(value, from), to);
            return new ExchangeCardDTO(exchangeResult);
        }
    }
}