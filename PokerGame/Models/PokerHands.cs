namespace PokerGame.Models
{
    public class PokerHands
    {
        public Hand Player1 { get; set; }
        public Hand Player2 { get; set; }
        public string DeckID { get; set; }

        public Hand Winner
        {
            get
            {
                if (Player1.Value > Player2.Value)
                {
                    return Player1;
                }
                else
                {
                    return Player2;
                }
            }
        }
    }


    public class Hand
    {
        public string Username { get; set; }
        public List<Card> Cards { get; set; }
        
        public int Value
        {
            get
            {
                int max = 0;
                foreach (Card card in Cards)
                {
                    if (card.Value > max)
                    {
                        max = card.Value;
                    }
                }
                return max;
            }
        }
        public Hand()
        {
            Cards = new List<Card>();
        }


    }

    public class Card
    {
        public string Suit { get; set; } // H, S, D, C
        public int Rank { get; set; } // 2,3,...10, J=11, Q=12, K=13, A=14
        public string Image { get; set; }
        /*
             * Rankings
             *  Hearts = 4, Spades = 3, Diamonds = 2, Clubs = 1
             *  Cards will just have their 2 through 13 rank
             *  We'll multiply the suit by 14 and add on the card's rank
             */
        public int Value
        {
            get
            {
                int suitvalue = 0;
                if (Suit == "H")
                {
                    suitvalue = 4;
                }
                else if (Suit == "S")
                {
                    suitvalue = 3;
                }
                else if (Suit == "D")
                {
                    suitvalue = 2;
                }
                else if (Suit == "C")
                {
                    suitvalue = 1;
                }
                return suitvalue * 14 + Rank;
            }
        }
    }


}
