using CurrencyExchange.Model;
using CurrencyExchange.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Service.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class CurrencyExchangeController : ControllerBase
    {

        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCurrencies")]
        public IEnumerable<CurrencyDTO> GetCurrencyList()
        {
            // TODO: Switch to using Model as opposed to hardcoded list here

            return new List<CurrencyDTO>()
            {
                new CurrencyDTO("GBP", "British Pounds"),
                new CurrencyDTO("USD", "US Dollars")
            };
        }

        [HttpGet(Name = "ConvertMoney")]
        public Money Exchange(string from, decimal value, string to)
        {
            // TODO: Validate Currency To and From exists

            // TODO: Return Exchange Result is Exchange Successful

            // TODO: Return appropriate HTTP response is exchange unsuccessful


            // TODO: Switch to using Model when created: Simply get a result for NOW
            return new Money(100.02M, "GBP");
        }
    }
}