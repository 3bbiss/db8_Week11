using CardsLabDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CardsLabDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // This worked at first, but we had issues. Only 1 deck
        // shared across all users of our site.
        // Let's give each user their own deck.
        // Instead of keeping a single, static deck ID, we'll
        // "pass it around" through our views and links.
        //public static string DeckId = string.Empty;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient web = new HttpClient();
            web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");
            var connection = await web.GetAsync($"new/shuffle/?deck_count=1");
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();
            //DeckId = resp.deck_id; // Not doing this after all.

            connection = await web.GetAsync($"{resp.deck_id}/draw/?count=5");
            resp = await connection.Content.ReadAsAsync<CardResponse>();

            return View(resp);
        }

        public async Task<IActionResult> DrawFive(string deck_id)
        {
            HttpClient web = new HttpClient();
            web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");
            var connection = await web.GetAsync($"{deck_id}/draw/?count=5");
            if (connection.StatusCode.ToString() == "NotFound")
            {
                return Content(connection.RequestMessage.RequestUri.OriginalString);
            }
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();
            return View("index", resp);
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