using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASolitare
{
    internal class Card
    {
        private string rank, suit, colour;
        private int rankNo, facing;

        /// <summary>
        /// Instanciates a new instance of the Card class
        /// </summary>
        /// <param name="rankNo">The rank of the card as an integer</param>
        /// <param name="suit">The suit of the card</param>
        /// <param name="facing">Whether the card is visible or hidden</param>
        public Card(int rankNo, string suit, int facing)
        {
            this.rankNo = rankNo;
            this.suit = suit;
            this.facing = facing;

            if (rankNo == 1)
            {
                rank = "A";
            }
            else if (rankNo == 11)
            {
                rank = "J";
            }
            else if (rankNo == 12)
            {
                rank = "Q";
            }
            else if (rankNo == 13)
            {
                rank = "K";
            } 
            else
            {
                rank = rankNo.ToString();
            }

            if (suit == "D" ^ suit == "H")
            {
                colour = "red";
            }
            else
            {
                colour = "black";
            }
        }

        /// <summary>
        /// Gives the card's stats in a displayable form
        /// </summary>
        /// <returns>Returns the card as it is to be displayed to the user</returns>
        public string Display() 
        {
            if (rankNo == 10)
            {
                return " " + rank + suit + " ";
            }
            else
            {
                return " " + rank + " " + suit + " ";
            }
        }

        public void SetFacing(int facing) {this.facing = facing;}

        public string GetRank() {return rank;}
        public string GetSuit() {return suit;}
        public int GetRankNo() {return rankNo;}
        public int GetFacing() {return facing;}
        public string GetColour() {return colour;}
    }
}
