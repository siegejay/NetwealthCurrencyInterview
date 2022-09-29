using CurrencyExchange.Site.Models;
using ExchangeBureau;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurrencyExchange.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrencyExchangeClient _exchangeService;

        public HomeController(ILogger<HomeController> logger, ICurrencyExchangeClient exchangeService)
        {
            _logger = logger;
            _exchangeService = exchangeService;
        }

        [HttpGet, ActionName("Index"), Route("")]
        public async Task<IActionResult> Index(string fromCode = "GBP", decimal value = 0M, string toCode = "USD", decimal? exchangedValue = null)
        {
            // Get list of Currencies
            var currencyList = await _exchangeService.CurrencyAsync();
            var viewModel = new HomeViewModel(currencyList.ToList());
            viewModel.ExchangeFromCode = fromCode;
            viewModel.ExchangeToCode = toCode;
            viewModel.FromValue = value;
            viewModel.ToValue = exchangedValue;
            return View(viewModel);
        }

        [HttpPost, ActionName("Calculate")]
        public async Task<IActionResult> CalculateExchangeValue(string exchangeFromCode, decimal fromValue, string exchangeToCode)
        {
            // Get Exchange Value from the service
            var exchangeValue = await _exchangeService.ExchangeAsync(exchangeFromCode, (double)fromValue, exchangeToCode);

            // TODO: Handle errors

            return RedirectToAction("Index", new RouteValueDictionary() {
                {"fromCode", exchangeFromCode },
                {"toCode", exchangeToCode },
                {"value", fromValue },
                {"exchangedValue", exchangeValue.To.Value }
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}