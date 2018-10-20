using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCG_Game_Final
{
    public class Deck
    {
        //VARIABLES
        //Constant Variables. Eye-Candy.
        const int MIN=1, MAX=13, TOTAL_CARDS=54;
        //Randomizer used in Shuffle method.
        Random rng = new Random();
        /// <summary>
        /// Defines the list SUITS found in Cards.
        /// </summary>
        static List<char> SUITS = new List<char>() { 'S', 'H', 'D', 'C' };
        /// <summary>
        ///     <para>A List of Cards creates a deck. Initialized in the constructor.</para>
        /// <para>deck.Add(Value, Suit)</para>
        /// <para>deck[index].Value.Suit.Face</para>
        /// </summary>
        public List<Card> deck;
        //---------------------
        //User Testing Methods
        /// <summary>
        /// <para>Displays the current state of the deck.</para>
        /// </summary>
        public void showDeck()
        {
            //deck[0].Face = true;
            //deck[2].Face = true;
            //deck[14].Face = true;
            //deck[27].Face = true;
            //deck[40].Face = true;
            //deck[53].Face = true;
            for (int x=0; x<deck.Count; x++)
            {
                deck[x].Face = true;
                if (deck[x].Face == false)
                {
                    Console.WriteLine("DOWN");
                }
                else
                {
                    Console.WriteLine(deck[x].Value + "" + deck[x].Suit);
                }
            }
        }

        /// <summary>
        /// Shows desired card from the deck. Implement a fail-safe to repeat until they pick a card in range.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Card Peek(int position)
        {
            if (position < deck.Count)
            {
                Card cardPeeked = deck[position];
                return cardPeeked;
            }
            return new Card(position, ' ');
            
        }

        public int Length()
        {
            return deck.Count;
        }

        //---------------------
        //CONSTRUCTORS
        /// <summary>
        /// <para>Constructor will initialize the deck by adding Cards to it.</para>
        /// </summary>
        public Deck()
        {
            //Create a new Deck.
            deck = new List<Card>();

            //Add two Jokers (0, 'J') Cards.
            deck.Add(new Card(0, 'J'));
            deck.Add(new Card(0, 'J'));
            foreach(char suit in SUITS)
            {
                for(int x=MIN; x<=MAX; x++)
                {
                    deck.Add(new Card(x, suit));
                }
            }
        }

        public Deck(Deck other)
        {
            deck = new List<Card>(other.deck);
        }

        public Deck(bool empty)
        {
            deck = new List<Card>();
        }

        //---------------------
        //GAME METHODS

        /// <summary>
        /// <para>Shuffles deck.</para>
        /// <para>Will use randomizer "rng" variable.</para>
        /// </summary>
        public void Shuffle()
        {
            for (int x = deck.Count - 1; x > 0; x--)
            {
                int swap = rng.Next(0, x);
                Card temp = deck[x];
                deck[x] = deck[swap];
                deck[swap] = temp;
            }
            //deck.OrderBy(item => rng.Next());
        }

        /// <summary>
        /// <para>Assigns the card on top of the deck to cardDrawn</para>
        /// <para>It then removes said card from the top of the deck.</para>
        /// </summary>
        /// <returns>Card cardDrawn</returns>
        public Card Draw()
        {
            Card cardDrawn = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return cardDrawn;
        }

        /// <summary>
        /// <para>Will add a selected card to a deck.</para>
        /// <para>Will mostly be used to add to the discard deck from the player's hands.</para>
        /// </summary>
        /// <param name="newCard"></param>
        public void Add(Card newCard)
        {
            deck.Add(newCard);
        }

        /// <summary>
        /// <para>Determines if a deck is empty. If the deck.Count is 0, it contains no elements.</para>
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            if(deck.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
