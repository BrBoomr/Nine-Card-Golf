using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCG_Game_Final
{
    
    public class Hand
    {
        //VARIABLES
        /// <summary>
        /// <para>Holds the player's cards in the field.</para> 
        /// <para>Total of 9 in 3x3 grid.</para>
        /// </summary>
        public List<Card> hand;
        public Deck shared_Deck;
        
        //----------------------
        //CONSTRUCTORS
        /// <summary>
        /// <para>Constructor initializing a player's hand.</para>
        /// <para>Draws a total of 9 cards in the 3x3 field.</para>
        /// <para>Subsequentially, removes 9 cards from deck List.</para>
        /// </summary>
        public Hand()
        {
            hand = new List<Card>();
            for (int x = 0; x < 9; x++)
            {
                Add(shared_Deck.Draw());
            }
        }

        public Hand(Deck shared_Deck)
        {
            hand = new List<Card>();
            
            for (int x = 0; x < 9; x++)
            {
                Add(shared_Deck);
            }
            this.shared_Deck = shared_Deck;
        }

        //--------------------
        //User Testing Methods
        public void showHand()
        {
            //hand[0].Face = true;
            //hand[4].Face = true;
            //hand[8].Face = true;
            for (int x=0; x<9; x++)
            {
                if (hand[x].Face==true)
                {
                    if (x == 0) { Console.Write("\t\t"); Console.Write("\t\t"); }
                    if (x == 2 || x == 5)
                    {
                        Console.WriteLine("[" + hand[x].Value + "" + hand[x].Suit + "] ");
                        Console.Write("\t\t"); Console.Write("\t\t");
                    }
                    else
                    {                      
                        Console.Write("[" + hand[x].Value + "" + hand[x].Suit + "] ");
                    }
                }
                else
                {
                    if (x == 0) { Console.Write("\t\t"); Console.Write("\t\t"); }
                    if (x == 2 || x == 5)
                    {
                        Console.WriteLine("[-] ");
                        Console.Write("\t\t"); Console.Write("\t\t");
                    }
                    else
                    {
                        Console.Write("[-] ");
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Shows desired card from the hand. Implement a fail-safe to repeat until they pick a card in range.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Card Peek(int position)
        {
            if (position < hand.Count)
            {
                Card cardPeeked = hand[position];
                return cardPeeked;
            }
            return new Card(position, ' ');
        }


        //----------------------
        //Game Methods
        public void Add()
        {
            hand.Add(shared_Deck.Draw());
        }

        /// <summary>
        /// Adding by Drawing from the deck.
        /// </summary>
        /// <param name="shared_Deck"></param>
        public void Add(Deck shared_Deck)
        {
            hand.Add(shared_Deck.Draw());
        }

        /// <summary>
        /// Adding by obtaining a card by other means.
        /// </summary>
        /// <param name="newCard"></param>
        new public void Add(Card newCard)
        {
            hand.Add(newCard);  
        }

        /// <summary>
        /// <para>Receives a position from user and assigns it as an index to access property Face.</para>
        /// <para>hand[position].Face will be changed to True.</para>
        /// </summary>
        /// <param name="position"></param>
        public void TurnUp(int position)
        {   
            hand[position].Face = true;
        }

        public Card Replace(int position, Deck deck_choice)
        {
            Card replaced = hand[position];
            replaced.Face = true;
            hand[position] = deck_choice.Draw();
            hand[position].Face = true;

            return replaced;

        }

        /// <summary>
        /// <para>Evaluates if the user has chosen a card in "DOWN" position.</para>
        /// <para>If hand[position].Face evaluates to true, it will return true.</para>
        /// <para>Otherwise, it will let the game continue.</para>
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsFaceUp(int position)
        {
            if (hand[position].Face == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Evaluates if all the Cards in the player-hand have a Face property of True. If so, returns True.
        /// </summary>
        /// <returns></returns>
        public bool IsDone()
        {
            for(int x=0; x<9; x++)
            {
                if (hand[x].Face==false)
                {
                    return false;
                }
            }
            return true;
            
        }

        public int Score()
        {
            /*
             * 0 1 2
             * 3 4 5
             * 6 7 8
             * */
            int score = 0;
            //Initial scoring. Will be then be modified after checking three-pairs in directionals.
            for(int x=0; x<9; x++)
            {
                if(hand[x].Face == true)
                {
                   score += hand[x].Card_Score();
                }
            }
            //DOWN DIAGONAL
            if(hand[0].Face && hand[4].Face && hand[8].Face)
            {
                if(hand[0].Value == hand[4].Value && hand[4].Value == hand[8].Value)
                {
                    score -= hand[0].Card_Score() * 3;
                }
            }
            //LINE 1 STRAIGHT
            if(hand[0].Face && hand[1].Face && hand[2].Face)
            {
                if (hand[0].Value == hand[1].Value && hand[1].Value == hand[2].Value)
                {
                    score -= hand[0].Card_Score() * 3;
                }

            }
            //LINE 2 STRAIGHT
            if (hand[3].Face && hand[4].Face && hand[5].Face)
            {
                if (hand[3].Value == hand[4].Value && hand[4].Value == hand[5].Value)
                {
                    score -= hand[3].Card_Score() * 3;
                }

            }

            //LINE 3 STRAIGHT
            if (hand[6].Face && hand[7].Face && hand[8].Face)
            {
                if (hand[6].Value == hand[7].Value && hand[7].Value == hand[8].Value)
                {
                    score -= hand[6].Card_Score() * 3;
                }

            }

            //UP DIAGONAL
            if (hand[6].Face && hand[4].Face && hand[2].Face)
            {
                if (hand[6].Value == hand[4].Value && hand[4].Value == hand[2].Value)
                {
                    score -= hand[6].Card_Score() * 3;
                }

            }

            //LINE 1 DOWN
            if (hand[0].Face && hand[3].Face && hand[6].Face)
            {
                if (hand[0].Value == hand[3].Value && hand[3].Value == hand[6].Value)
                {
                    score -= hand[0].Card_Score() * 3;
                }

            }
            //LINE 2 DOWN
            if (hand[1].Face && hand[4].Face && hand[7].Face)
            {
                if (hand[1].Value == hand[4].Value && hand[4].Value == hand[7].Value)
                {
                    score -= hand[1].Card_Score() * 3;
                }

            }
            //LINE 3 DOWN
            if (hand[2].Face && hand[5].Face && hand[8].Face)
            {
                if (hand[2].Value == hand[5].Value && hand[5].Value == hand[8].Value)
                {
                    score -= hand[2].Card_Score() * 3;
                }

            }
            return score;
        }


    }
}
