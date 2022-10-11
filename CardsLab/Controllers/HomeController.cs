using CardsLab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CardsLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient web = new HttpClient();
            web.BaseAddress = new Uri("https://www.deckofcardsapi.com/");
            var connection = await web.GetAsync("api/deck/new/shuffle/?deck_count=1");

            try
            {
                CardResponse res = await connection.Content.ReadAsAsync<CardResponse>();
                return View(res);
            }
            catch (Exception e)
            {
                return View();
            }
        }


        public async Task<IActionResult> Draw()
        {
            HttpClient web = new HttpClient();
            web.BaseAddress = new Uri("https://www.deckofcardsapi.com/");
            var connection = await web.GetAsync("api/deck/tlocoh5289tn/draw/?count=5");

            try
            {
                List<Card> cards = await connection.Content.ReadAsAsync<List<Card>>();
                return View(cards);
            }
            catch (Exception e)
            {
                return View();
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}