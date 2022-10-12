using Microsoft.AspNetCore.Mvc;
using PokerGame.Models;
using System.Diagnostics;

namespace PokerGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        async public Task<IActionResult> DrawFive(string name1, string name2)
        {
            //Create a new deck
            string deck_id = await CardAPI.GetNewDeck();

            // Create an instance of PokerHands
            PokerHands poker = new PokerHands();

            // Draw 5 cards for user1
            Hand user1 = await CardAPI.GetHand(deck_id, 5);
            user1.Username = name1;
            poker.Player1 = user1;

            // Draw 5 cards for user 2
            Hand user2 = await CardAPI.GetHand(deck_id, 5);
            user2.Username = name2;
            poker.Player2 = user2;

            // And display ten cards and the name of the winner
            return View(poker);

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