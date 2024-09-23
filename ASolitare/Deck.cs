using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace ASolitare
{
    class Deck
    {
        private int size = 52;
        private List<Card> cardList = new(52);

        /// <summary>
        /// Instantiates a new instance of the deck class
        /// </summary>
        public Deck()
        {
            CreateCards();
        }

        /// <summary>
        /// Creates a full set of 52 cards and adds them to the list in order
        /// </summary>
        public void CreateCards()
        {
            for (int r = 1; r < 14; r++)
            {
                for (int s = 0; s < 4; s++)
                {
                    string suit = "D";

                    if (s == 1)
                    {
                        suit = "H";
                    }
                    else if (s == 2)
                    {
                        suit = "C";
                    }
                    if (s == 3)
                    {
                        suit = "S";
                    }
                    Card card = new(r, suit, 0);
                    cardList.Add(card);
                }
            }
        }

        /// <summary>
        /// Randomises the placement of the cards in the deck
        /// </summary>
        public void Shuffle()
        {
            var rand = new Random();
            List<Card> newList = new(size);
            for (int i = 0; i < size; i++)
            {
                int randNum = rand.Next(size - i);
                newList.Add(cardList[randNum]);
                cardList.Remove(cardList[randNum]);
            }
            cardList = newList;
        }

        /// <summary>
        /// Removes the last card from the deck
        /// </summary>
        /// <returns>The given card</returns>
        public Card DrawCard() 
        {
            Card card = cardList[size - 1];
            cardList.Remove(card);
            size -= 1;
            return card;
        }

        public int GetSize() { return size; }

    }

}
