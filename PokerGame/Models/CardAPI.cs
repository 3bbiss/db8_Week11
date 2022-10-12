namespace PokerGame.Models
{
    public class CardResponse
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        // We copied this class over but
        // changed the second class to APICard.
        // So change it in the list as well.
        public List<APICard> cards { get; set; }
        public int remaining { get; set; }
    }


    public class APICard
    {
        public string code { get; set; }
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
    }


    //
    //CardAPI Functionality
    //GetNewDeck() - return a Deck ID
    // GetHand(deck_id, count) - return a single Hand instance
    //     with the Card list all populated.

    public class CardAPI
    {
        public static HttpClient _web = null;

        public static HttpClient GetHttpClient()
        {

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

            var connection = await web.GetAsync($"new/shuffle/?deck_count=1");
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();

            return resp.deck_id;
        }


        public async static Task<Hand> GetHand(string deck_id, int count)
        {
            HttpClient web = GetHttpClient();
            var connection = await web.GetAsync($"{deck_id}/draw/?count={count}");
            CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();

            // Instead of just returning CardResponse instance, we'll
            // Create an instance of Hand and fill it, and return that.

            Hand newhand = new Hand();
            foreach (APICard apicard in resp.cards)
            {
                Card newcard = new Card();
                newcard.Image = apicard.image;
                newcard.Suit = apicard.suit.Substring(0,1);

                int cardvalue = 0;
                bool worked = int.TryParse(apicard.value, out cardvalue);
                if (!worked)
                {
                    if (apicard.value == "JACK")
                    {
                        cardvalue = 11;
                    }
                    else if (apicard.value == "QUEEN")
                    {
                        cardvalue = 12;
                    }
                    else if (apicard.value == "KING")
                    {
                        cardvalue = 13;
                    }
                    else if (apicard.value == "ACE")
                    {
                        cardvalue = 14;
                    }
                }
                newcard.Rank = cardvalue;
                // We just made a new instance of Card; let's add it to 
                // the hand's list
                newhand.Cards.Add(newcard);
            }
            return newhand;

        }

    }

}
