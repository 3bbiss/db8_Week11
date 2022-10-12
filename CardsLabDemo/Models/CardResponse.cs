namespace CardsLabDemo.Models
{
    public class CardResponse
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public List<Card> cards { get; set; }
        public int remaining { get; set; }
    }

    public class Card
    {
        public string code { get; set; }
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
    }


    public class CardAPI
    {
        // This is the DAL for the API calls
        // Let's encapsulate the API calls into easy-to-use methods
        // We'll have two methods
            // GetNewDeck()
            // GetCards(deck_id, count)

        // DO NOT access this variable directly.
        // When you need an HttpClient instance, call GetHttpClient()
        public static HttpClient _web = null;

        public static HttpClient GetHttpClient()
        {
            // We call this concept a "singleton"

            // First check if we already have an HttpClient.
            // If so, return that one.
            // Else create one and return that one.
            //
            // Or, flip the logic
            //
            // First check if we already have an HttpClient.
            // If not, create one.
            // Then regardless, return it.
            

            if (_web == null)
            {
                _web = new HttpClient();
                _web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");

            }

            return _web;
        }

        public static async Task<String> GetNewDeck()
        {
            HttpClient web = GetHttpClient();
            // Removed this one below (that broke me earlier at noon)
            // because we're setting the base address in GetHttpClient()
            //web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");


            var connection = await web.GetAsync($"new/shuffle/?deck_count=1");
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();

            return resp.deck_id;
        }

        public static async Task<CardResponse> GetCards(string deck_id, int count)
        {
            HttpClient web = GetHttpClient();
            var connection = await web.GetAsync($"{deck_id}/draw/?count={count}");
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();

            return resp;
        }



    }


}
