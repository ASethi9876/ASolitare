using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASolitare
{
    internal class Pile
    {
        private List<Card> cardList = new();
        private int size = 0, startSize = 0;

        /// <summary>
        /// Instantiates a new instance of the Pile class
        /// </summary>
        /// <param name="startSize">The initial size of the pile</param>
        public Pile(int startSize)
        {
            this.startSize = startSize;
        }

        /// <summary>
        /// Adds a card to the end of the pile
        /// </summary>
        /// <param name="card">The card to be added</param>
        public void AddCard(Card card)
        {
            cardList.Add(card);
            size += 1;
        }

        /// <summary>
        /// Removes the last card in the pile and sets the new end to be facing up
        /// </summary>
        /// <returns>The card to be removed, or null if empty</returns>
        public Card RemoveCard()
        {
            Card card = null;
            if (size > 0)
            {
                card = cardList[size - 1];
                cardList.Remove(card);
                size -= 1;
            }
            if (size > 0)
            {
                cardList[size - 1].SetFacing(1);
            }
            return card;

        }

        /// <summary>
        /// Returns a card at a specific point in the pile
        /// </summary>
        /// <param name="i">The index of the card</param>
        /// <returns>The given card, or null if empty</returns>
        public Card GetCard(int i)
        {
            if (size > 0)
            {
                return cardList[i];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the last card in the pile
        /// </summary>
        /// <returns>The given card, or null if empty</returns>
        public Card GetLast() 
        {
            if (size > 0)
            {
                return cardList[size - 1];
            }
            else
            {
                return null;
            }
        }

           public int GetSize() {return size;}
    }

}
