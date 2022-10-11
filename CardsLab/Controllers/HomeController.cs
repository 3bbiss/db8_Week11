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
            CardResponse res = await connection.Content.ReadAsAsync<CardResponse>();

            var connectionDraw = await web.GetAsync($"api/deck/{res.deck_id}/draw/?count=5");

            res = await connectionDraw.Content.ReadAsAsync<CardResponse>();

            return View(res.cards);

            //try
            //{
            //    CardResponse res = await connection.Content.ReadAsAsync<CardResponse>();
            //    return View();
            //}
            //catch (Exception e)
            //{
            //    return View();
            //}
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