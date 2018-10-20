using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NCG_Game_Final
{
    public class Game
    {
        private int TIMER = 500;
        public Deck main_deck, discard_deck;
        public Player p1, p2;
        public Hand h1, h2;
        Random rng;
        public Game()
        {
            rng = new Random();
            main_deck = new Deck();
            discard_deck = new Deck(true);
            main_deck.Shuffle();
            

            h1 = new Hand(main_deck);
            h2 = new Hand(main_deck);

            p1 = new Player(h1);
            p2 = new Player(h2);

            discard_deck.Add(main_deck.Draw());


        }

        public void showDeckStatus()
        {
            Console.WriteLine();
            Console.Write("\t\t"); Console.Write("\t\t");
            Console.WriteLine("MDCount:[" + main_deck.Length() + "]------------------" +
                "DDTop:[" +
                discard_deck.Peek(discard_deck.Length() - 1).Value + "" +
                discard_deck.Peek(discard_deck.Length() - 1).Suit + "]");
            Console.WriteLine();
        }
        public void Display_Hands()
        {
            Console.Write("\t\t"); Console.Write("\t\t");
            Console.WriteLine();
            Console.WriteLine("********************************************");

            Console.Write("\t\t"); Console.Write("\t\t"); Console.WriteLine("PLAYER 1|Score: "+h1.Score());
            h1.showHand();

            showDeckStatus();

            Console.Write("\t\t"); Console.Write("\t\t"); Console.WriteLine("PLAYER 2|Score: "+h2.Score());
            h2.showHand();
            Console.WriteLine();

        }
        public void diagram()
        {
            for (int x = 0; x < 9; x++)
            {
                if (x == 0) { Console.Write("\t\t"); Console.Write("\t\t"); }
                if (x == 2 || x == 5)
                {
                    Console.WriteLine(x + " ");
                    Console.Write("\t\t"); Console.Write("\t\t");
                }
                else
                {
                    Console.Write(x + " ");
                }
            }
            Console.WriteLine();
        }

        public String showWinner()
        {
            int winner = 0;
            if(h1.Score() > h2.Score())
            {
                winner = 2;
            }  
            else if(h1.Score() == h2.Score())
            {
                winner = 3;
            }
            else
            {
                winner = 1;
            }

            if (winner == 3)
            {
                return "DRAW";
            }
            else
            {
                return ("Player " + winner + " Wins!");
            }
        }

        public void Play()
        {
            bool firstTurn = true;
            
            while (true)
            {    
                Console.WriteLine("Player 1 Turn");
                p1.TakeTurn(main_deck, discard_deck, firstTurn, rng);

                if (h1.IsDone() == true || h2.IsDone() == true)
                {
                    showWinner();
                    break;
                }
                Console.WriteLine("Player 2 Turn");

                p2.TakeTurn(main_deck, discard_deck, firstTurn, rng);

                if (h1.IsDone() == true || h2.IsDone() == true)
                {
                    showWinner();
                    break;
                }
                firstTurn = false;
            }
            
        }

        public Card HandCardChosen(int player, int position)
        {
            if (player == 0)
            {
                h1.TurnUp(position);
                return h1.hand[position];
            }
            h2.TurnUp(position);
            return h2.hand[position];
        }
    }
}
