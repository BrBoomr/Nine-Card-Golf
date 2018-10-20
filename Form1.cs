using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCG_Game_Final
{
    public partial class Form1 : Form
    {
        Game _game;
        Image img_back = Image.FromFile(@"../../CardImages/cardback.png");
        Card _cardDrawn;
        int turn = 0;
        int deck_choice = 0;
       
        public Form1()
        {
            _game = new Game();
            //_game.Play();
            InitializeComponent();
            SetDeck(); 
            defaultCards();
        }
        private void SetDeck()
        {
            string cardName = _game.discard_deck.Peek(_game.discard_deck.Length() - 1).Name;
            Image discard_img = Image.FromFile(@"../../CardImages/" + cardName + ".png");
            D_Deck.Image = discard_img;
            M_Deck.Image = Image.FromFile(@"../../CardImages/cardback.png");
            P1Draw.Image = null;
            P2Draw.Image = null;
        }

        private void defaultCards()
        {
            Image def = Image.FromFile(@"../../CardImages/cardback.png");
            //HAND 1
            setCardImage(def, label3);   //00
            setCardImage(def, label4);   //01
            setCardImage(def, label5);
            setCardImage(def, label6);
            setCardImage(def, label7);
            setCardImage(def, label8);
            setCardImage(def, label9);
            setCardImage(def, label10);
            setCardImage(def, label11);
            //HAND 2
            setCardImage(def, label12);  //10
            setCardImage(def, label13);  //11
            setCardImage(def, label14);
            setCardImage(def, label15);
            setCardImage(def, label16);
            setCardImage(def, label17);
            setCardImage(def, label18);
            setCardImage(def, label19);
            setCardImage(def, label20);
        }

        private void setCardImage(Image img, Label l)
        {
            l.Size = new Size(img.Width, img.Height);
            l.Image = img;
        }

        
        //HAND CLICK EVENT-------------------------------------------
        private void HandClicked(object sender, EventArgs e)
        {
            label21.Text = "Current Turn: \n" + turn;
            Label l = sender as Label;
            if(P1Draw.Image==null && P2Draw.Image == null)
            {
                HandClicked_TurnUp(l);
            }
            else
            {
                HandClicked_Replace(l);
            }

            if(_game.h1.IsDone() || _game.h2.IsDone())
            {

                label21.Text = _game.showWinner();
            }
        }

        public void HandClicked_TurnUp(Label l)
        {
            string _tag = l.Tag as string;
            Console.WriteLine(_tag);
            if (Convert.ToInt32(_tag.Substring(0, 1)) == turn)
            {
                int player = Convert.ToInt32(_tag.Substring(0, 1));        //Two Players
                int position = Convert.ToInt32(_tag.Substring(1, 1));      //Nine Cards

                Card cardClicked = _game.HandCardChosen(player, position);
                Image img = Image.FromFile(@"../../CardImages/" + cardClicked.Name + ".png");
                setCardImage(img, l);

                p1Score.Text = Convert.ToString(_game.h1.Score());
                p2Score.Text = Convert.ToString(_game.h2.Score());

                
                turn = (turn == 0) ? 1 : 0;
                label21.Text = "Current Turn: \n" + turn;
            }
            else
            {
                label21.Text = "Not Your Turn!";
            }


        }

        public void HandClicked_Replace(Label l)
        {
            string _tag = l.Tag as string;
            Console.WriteLine(_tag);
            if (Convert.ToInt32(_tag.Substring(0, 1)) == turn)
            {
                int player = Convert.ToInt32(_tag.Substring(0, 1));        //Two Players
                int position = Convert.ToInt32(_tag.Substring(1, 1));      //Nine Cards

                if (player == 0)
                {
                    HandClicked_Replace_P1(l, position);
                }
                else if (player == 1)
                {
                    HandClicked_Replace_P2(l, position);
                }

                p1Score.Text = Convert.ToString(_game.h1.Score());
                p2Score.Text = Convert.ToString(_game.h2.Score());

                turn = (turn == 0) ? 1 : 0;
                label21.Text = "Current Turn: \n" + turn;
            }
        }

        public void HandClicked_Replace_P1(Label l, int position)
        {
            Card replaced = _game.h1.hand[position];
            _game.p1.R_Choice(deck_choice, position, _game.main_deck, _game.discard_deck);

            Console.WriteLine(replaced.Name);
            _game.h1.TurnUp(position);
            l.Image = Image.FromFile(@"../../CardImages/" + _cardDrawn.Name + ".png");
            D_Deck.Image = Image.FromFile(@"../../CardImages/" + replaced.Name + ".png");
            P1Draw.Image = null;
        }

        public void HandClicked_Replace_P2(Label l, int position)
        {
            Card replaced = _game.h2.hand[position];
            _game.p2.R_Choice(deck_choice, position, _game.main_deck, _game.discard_deck);

            _game.h2.TurnUp(position);
            l.Image = Image.FromFile(@"../../CardImages/" + _cardDrawn.Name + ".png");
            D_Deck.Image = Image.FromFile(@"../../CardImages/" + replaced.Name + ".png");
            P2Draw.Image = null;
        }
        //-------------------------------------------------------------
        
        //DECK CLICK EVENT---------------------------------------------
        private void DeckClick(object sender, EventArgs e)
        {
            label21.Text = "Current Turn: \n" +turn;
            Label deck = sender as Label;

            if (deck.Name == M_Deck.Name)
            {
                DeckClick_Main();
            }

            else if(deck.Name == D_Deck.Name)
            {
                DeckClick_Discard();
            }
            label21.Text = _game.showWinner();
        }

        public void DeckClick_Main()
        {
            deck_choice = 1;
            if (_game.main_deck.isEmpty())
            {
                M_Deck.Image = null;
                Console.WriteLine("Deck is Empty!");
            }

            _cardDrawn = _game.main_deck.Peek(_game.main_deck.Length() - 1);
            Image img = Image.FromFile(@"../../CardImages/" + _cardDrawn.Name + ".png");
            if (turn == 0)
            {
                M_Deck.Size = new Size(img.Width, img.Height);
                P1Draw.Image = img;
            }
            else
            {
                M_Deck.Size = new Size(img.Width, img.Height);
                P2Draw.Image = img;
            }
        }

        public void DeckClick_Discard()
        {
            _cardDrawn = _game.discard_deck.Peek(_game.discard_deck.Length() - 1);
            Image img = Image.FromFile(@"../../CardImages/" + _cardDrawn.Name + ".png");
            deck_choice = 2;
            if (_game.discard_deck.isEmpty())
            {
                D_Deck.Image = null;
                Console.WriteLine("Deck is Empty!");
            }
            if (turn == 0)
            {
                D_Deck.Size = new Size(img.Width, img.Height);
                P1Draw.Image = img;
            }
            else
            {
                D_Deck.Size = new Size(img.Width, img.Height);
                P2Draw.Image = img;
            }
        }
        //-------------------------------------------------------------
        private void label21_Click(object sender, EventArgs e)
        {
            //We need to automate this process so that it grabs ANY label and assigns it
            //The according card_Name().
            //We also need to link all labels to the cards in player's hands.
            //Link by tag? hand[0] == label.tag(0,0) ? incompatible types, but we can trick it?
            Label l = sender as Label;
            Card test = new Card(5, 'D');
            // l.Text = test.Card_Name();
            Image img = Image.FromFile(@"../../CardImages/" + test.Name + ".png");
            l.Size = new Size(img.Width, img.Height);
            l.Image = img;
        }

    }
}
