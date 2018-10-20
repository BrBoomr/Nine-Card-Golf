using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCG_Game_Final
{
    public class Player
    {
        //Random rng = new Random();
        Hand h;
        public Player(Hand player_Hand)
        {
            h = player_Hand;
        }

        /// <summary>
        /// <para>Action taken as the first turn of the game. Flips 3 cards for both players.</para>
        /// </summary>
        public void fTurn(Random rng)
        {
            int position = 0;
            for (int x = 3; x > 0; x--)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter " + x + "/3 Card-Positions to Flip: ");
                    //position = Convert.ToInt32(Console.ReadLine());
                    position = rng.Next(9);
                    if (h.IsFaceUp(position))
                    {
                        Console.WriteLine("Card Already Flipped. Pick Again!");
                        continue;
                    }
                    h.TurnUp(position);
                    h.showHand();
                    break;
                }
            }
        }

        /// <summary>
        /// <para>Turns over a card in hand decided by position.</para>
        /// </summary>
        /// <param name="position"></param>
        public void TU_Choice(int position, Random rng)
        {
            while (true)
            {
                Console.WriteLine("Enter Position: ");
                //position = Convert.ToInt32(Console.ReadLine());
                position = rng.Next(9);
                if (h.Peek(position).Face)
                {
                    Console.WriteLine("Card Already Flipped. Pick Again!");
                    continue;
                }
                h.TurnUp(position);
                break;
            }
        }

        /// <summary>
        /// <para>If the player chooses to Draw from either deck, then it MUST replace the card somewhere or discard it.</para>
        /// <para>If the player chooses to Draw from Main, it will give them the option to replace his own or discard.</para>
        /// <para>If the player chooses to Draw from Discard, it MUST replace the card with his own.</para>
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="position"></param>
        /// <param name="main"></param>
        /// <param name="discard"></param>
        public void R_Choice(int choice, int position, Deck main, Deck discard, Random rng)
        {
            Card cardDrawn;
            Console.WriteLine("Draw from Deck [1] Main [2] Discard: ");
            while (true)
            {
                //choice = Convert.ToInt32(Console.ReadLine());
                choice = rng.Next(1, 3);
                //MAIN DECK
                if (choice == 1)
                {
                    cardDrawn = main.Peek(main.Length() - 1);
                    Console.WriteLine("Drawn: [" + cardDrawn.Value + "" + cardDrawn.Suit + "]");
                    Console.WriteLine("[1] Replace [2] Discard");
                    //choice = Convert.ToInt32(Console.ReadLine());
                    choice = rng.Next(1, 3);
                    if (choice == 1)
                    {
                        Console.WriteLine("Enter Position: ");
                        //position = Convert.ToInt32(Console.ReadLine());
                        position = rng.Next(9);
                        discard.Add(h.Replace(position, main));
                    }
                    else if (choice == 2)
                    {
                        discard.Add(main.Draw());
                    }
                }
                //DISCARD DECK
                else if (choice == 2)
                {
                    cardDrawn = discard.Peek(discard.Length() - 1);
                    Console.WriteLine("Drawn: " + cardDrawn.Value + "" + cardDrawn.Suit);
                    Console.WriteLine("Enter Position: ");
                    //position = Convert.ToInt32(Console.ReadLine());
                    position = rng.Next(9);
                    discard.Add(h.Replace(position, discard));
                }
                break;
            }
        }

        /// <summary>
        /// <para>Initial menu for player's turns. Provides the options to proceed in their turn.</para>
        /// </summary>
        /// <param name="main"></param>
        /// <param name="discard"></param>
        /// <param name="firstTurn"></param>
        public void TakeTurn(Deck main, Deck discard, bool firstTurn, Random rng) 
        {
            int choice = 0;
            int position = 0;
            if (firstTurn)
            {
                fTurn(rng);
            }
            else
            {
                Console.WriteLine("[1] Turn Card Over (Positions 0-8)");
                Console.WriteLine("[2] Replace Any Card (Positions 0-8)");
                while (true)
                {
                    //choice = Convert.ToInt32(Console.ReadLine());
                    choice = rng.Next(1,3);
                    if (choice == 1 || choice == 2)
                    {
                        switch (choice)
                        {
                            case 1:
                                TU_Choice(position, rng);
                                break;
                            case 2:
                                R_Choice(choice, position, main, discard, rng);
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
            }

        }

        //=======================================GUI IMPLEMENTATION. DISREGARD PREVIOUS===============================
        public void fTurn(int position)
        {
            for (int x = 3; x > 0; x--)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter " + x + "/3 Card-Positions to Flip: ");
                    if (h.IsFaceUp(position))
                    {
                        Console.WriteLine("Card Already Flipped. Pick Again!");
                        continue;
                    }
                    h.TurnUp(position);
                    break;
                }
            }
        }

        public void TU_Choice(int position) { 
            while (true)
            {
                Console.WriteLine("Enter Position: ");
                //position = Convert.ToInt32(Console.ReadLine());
                if (h.Peek(position).Face)
                {
                    Console.WriteLine("Card Already Flipped. Pick Again!");
                    continue;
                }
                h.TurnUp(position);
                break;
            }
        }

     
        public void R_Choice(int choice, int position, Deck main, Deck discard)
        {
            Card cardDrawn;
            Console.WriteLine("Draw from Deck [1] Main [2] Discard: ");
            Console.WriteLine(choice);
            while (true)
            {
                //MAIN DECK
                if (choice == 1)
                {
                    cardDrawn = main.Peek(main.Length() - 1);
                    Console.WriteLine("Drawn: [" + cardDrawn.Value + "" + cardDrawn.Suit + "]");
                    Console.WriteLine("Enter Position: ");
                    Console.WriteLine(position);
                    //position = Convert.ToInt32(Console.ReadLine());
                    discard.Add(h.Replace(position, main));
                }
                //DISCARD DECK
                else if (choice == 2)
                {
                    cardDrawn = discard.Peek(discard.Length() - 1);
                    Console.WriteLine("Drawn: " + cardDrawn.Value + "" + cardDrawn.Suit);
                    Console.WriteLine("Enter Position: ");
                    Console.WriteLine(position);
                    //position = Convert.ToInt32(Console.ReadLine());
                    discard.Add(h.Replace(position, discard));
                }
                break;
            }
        }

        public void TakeTurn(Deck main, Deck discard, bool firstTurn, int choice, int position)
        {
            if (firstTurn)
            {
                fTurn(position);
            }
            else
            {
                Console.WriteLine("[1] Turn Card Over (Positions 0-8)");
                Console.WriteLine("[2] Replace Any Card (Positions 0-8)");
                while (true)
                {
                    //choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2)
                    {
                        switch (choice)
                        {
                            case 1:
                                TU_Choice(position);
                                break;
                            case 2:
                                R_Choice(choice, position, main, discard);
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
            }

        }
    }
}
