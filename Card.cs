using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCG_Game_Final
{
    public class Card
    {
        public int Value { get; set; }
        public char Suit { get; set; }
        public bool Face { get; set; }
        public string Name { get; set; }

        public Card(int value, char suit)
        {
            Value = value;
            Suit = suit;
            Face = false;
            Name = Card_Name();
        }

        /// <summary>
        /// Card method that returns a score-value based on the Card's value.
        /// </summary>
        /// <returns></returns>
        public int Card_Score()
        {
            //JOKER
            if(Value == 0)
            {
                return -2;
            }
            //ACE
            else if (Value == 1)
            {
                return 1;
            }
            //KING
            else if(Value == 13)
            {
                return 0;
            }
            return Value;
        }

        public string Card_Name()
        {
            String name="NULL";
            if (Suit == 'J')
            {
                name = "joker";
                return name;
            }
            if (Value == 1)
            {
                name = "Aceof";
            }
            else if (Value == 2)
            {
                name = "Twoof";
            }
            else if (Value == 3)
            {
                name = "Threeof";
            }
            else if (Value == 4)
            {
                name = "Fourof";
            }
            else if (Value == 5)
            {
                name = "Fiveof";
            }
            else if (Value == 6)
            {
                name = "Sixof";
            }
            else if(Value == 7)
            {
                name = "Sevenof";
            }
            else if (Value == 8)
            {
                name = "Eightof";
            }
            else if (Value == 9)
            {
                name = "Nineof";
            }
            else if(Value == 10)
            {
                name = "Tenof";
            }
            else if(Value == 11)
            {
                name = "Jackof";
            }
            else if(Value == 12)
            {
                name = "Queenof";
            }
            else if(Value == 13)
            {
                name = "Kingof";
            }

            //Append Suit
            if (Suit == 'S')
            {
                name += "Spades";
            }
            else if(Suit == 'D')
            {
                name += "Diamonds";
            }
            else if(Suit == 'C')
            {
                name += "Clubs";
            }
            else if(Suit == 'H')
            {
                name += "Hearts";
            }
            Name = name;
            return Name;
        }

    }
}
