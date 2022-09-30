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
        public async Task<IActionResult> Index(string fromCode = "GBP", decimal value = 0M, string toCode = "USD", bool includeResult = false)
        {
            // Get list of Currencies
            var currencyList = await _exchangeService.CurrencyAsync();

            // Check if this is being called on behalf on a new Submission - Redirect from successful Post
            CurrencyExchangeInputModel? inputModel = new CurrencyExchangeInputModel()
            {
                ExchangeFromCode = fromCode,
                FromValue = value,
                ExchangeToCode = toCode
            };

            ExchangeCardDTO? exchangeCard = null;
            if (includeResult)
            {
                // Get Exchange Value from the service
                exchangeCard = await _exchangeService.ExchangeAsync(fromCode,
                    (double)value,
                    toCode);
            }

            var viewModel = new HomeViewModel(currencyList.ToList(), inputModel, exchangeCard);
            return View(viewModel);
        }


        [HttpPost, ActionName("Calculate")]
        public IActionResult CalculateExchangeValue([Bind(Prefix = "ExchangeForm")] CurrencyExchangeInputModel? inputModel)
        {

            if (inputModel == null) return RedirectToAction("Index");

            return RedirectToAction("Index", 
                new RouteValueDictionary() 
                {
                    {"fromCode", inputModel.ExchangeFromCode},
                    {"toCode", inputModel.ExchangeToCode },
                    {"value", inputModel.FromValue },
                    {"includeResult", true }
                });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}