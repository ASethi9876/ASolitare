using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASolitare
{
    internal class Game
    {
        /// <summary>
        /// The main program to run the game
        /// </summary>
        /// <param name="args">None</param>
        static void Main(string[] args)
        {
            Deck deck = new();
            deck.Shuffle();

            List<Pile> tableau = CreatePiles();
            List<Pile> foundations = CreateFoundations();
            Pile waste = new(0);
            PlaceCards(deck, tableau);
            Pile stock = CreateStock(deck);

            bool playing = true;
            while (playing == true)
            {
                Card[,] displayList = CreateDisplay(tableau, foundations, waste);
                Display(displayList, stock.GetSize());
                string play = PromptUser(stock.GetSize());
                if (play == "d")
                {
                    DrawCard(stock, waste);
                }
                else if (play == "p")
                {
                    PutBack(stock, waste);
                }
                else
                {
                    MoveCheck(play, tableau, foundations, waste);
                }
                playing = WinCheck(tableau, stock.GetSize());
            }
            Console.WriteLine("You have won!");
        }

        /// <summary>
        /// Creates each pile in the tableau
        /// </summary>
        /// <returns>
        /// A list of 7 empty piles
        /// </returns>
        static List<Pile> CreatePiles()
        {
            Pile pile1 = new(1);
            Pile pile2 = new(2);
            Pile pile3 = new(3);
            Pile pile4 = new(4);
            Pile pile5 = new(5);
            Pile pile6 = new(6);
            Pile pile7 = new(7);
            return [pile1, pile2, pile3, pile4, pile5, pile6, pile7];
        }

        /// <summary>
        /// Creates each pile in the foundations
        /// </summary>
        /// <returns>A list of 4 empty piles</returns>
        static List<Pile> CreateFoundations()
        {
            Pile diamonds = new(0), hearts = new(0), clubs = new(0), spades = new(0);
            return [diamonds, hearts, clubs, spades];
        }

        /// <summary>
        /// Places the necessary of cards into each pile of the tableau
        /// </summary>
        /// <param name="deck">The starting deck of cards</param>
        /// <param name="tableau">The piles that the cards are to be placed in</param>
        static void PlaceCards(Deck deck, List<Pile> tableau)
        {
            for (int i = 0; i < tableau.Count; i++)
            {
                Pile pile = tableau[i];
                for (int j = 0; j <= i; j++) 
                {
                    if (deck.GetSize() > 0)
                    {
                        Card card = deck.DrawCard();
                        pile.AddCard(card);
                    }
                }
                Card lastCard = pile.GetLast();
                lastCard.SetFacing(1);
            }
        }

        /// <summary>
        /// Places the unused cards into the stock
        /// </summary>
        /// <param name="deck">The starting deck of cards</param>
        /// <returns>The stock of cards to be drawn from</returns>
        static Pile CreateStock(Deck deck)
        {
            Pile stock = new(deck.GetSize());
            while (deck.GetSize() > 0)
            {
                Card card = deck.DrawCard();
                stock.AddCard(card);
            }
            return stock;
        }

        /// <summary>
        /// Creates / updates a list of cards in the positions that they are displayed in
        /// </summary>
        /// <param name="tableau">The tableau</param>
        /// <param name="waste">The waste pile</param>
        /// <returns>The display as a 2d list of cards</returns>
        static Card[,] CreateDisplay(List<Pile> tableau, List<Pile> foundations, Pile waste)
        {
            Card[,] displayList = new Card[20, 17];
            for (int x = 0; x < 7; x++)
            {
                Pile pile = tableau[x];

                for (int y = 0; y < pile.GetSize(); y++)
                {
                    int xCoord = x;
                    xCoord = (xCoord * 2 + 2);
                    displayList[y, xCoord] = pile.GetCard(y);
                }
            }

            for (int f = 0; f < 4; f++)
            {
                int yCoord = f * 2;
                if (foundations[f].GetSize() > 0)
                {
                    displayList[yCoord, 16] = foundations[f].GetLast();
                }
            }

            if (waste.GetSize() > 0)
            {
                displayList[2, 0] = waste.GetLast();
            }

            return displayList;
        }

        /// <summary>
        /// Outputs the cards (visible or hidden) to the user
        /// </summary>
        /// <param name="displayList">The list of cards in the positions they are to be displayed</param>
        /// <param name="stockSize">The number of remaining cards in the stock</param>
        static void Display(Card[,] displayList, int stockSize)
        {
            for (int y = 0; y < 20; y++)
            {
                string lineString = "";
                for (int x = 0; x < 17; x++)
                {
                    Card card = displayList[y, x];
     
                    if (card != null)
                    {
                        if (card.GetFacing() == 1)
                        {
                            lineString += card.Display();
                        }
                        else
                        {
                            lineString += "#####";
                        }
                    }
                    else if (x == 0 & y == 0)
                    {
                        lineString += "DECK ";
                    }
                    else if (x == 0 & y == 1)
                    {
                        string sizeStr = stockSize.ToString();
                        if (stockSize > 9) 
                        {
                            lineString = " " + sizeStr + "  ";
                        }
                        else
                        {
                            lineString = "  " + sizeStr + "  ";
                        }
                    }
                    else if (x == 16)
                    {
                        if (y == 0)
                        {
                            lineString += "| D |";
                        }
                        else if (y == 2)
                        {
                            lineString += "| H |";
                        }
                        else if (y == 4)
                        {
                            lineString += "| C |";
                        }
                        else if (y == 6)
                        {
                            lineString += "| S |";
                        }
                        else
                        {
                            lineString += "-----";
                        }
                    }
                    else 
                    {
                        lineString += "-----";
                    }
                }
                Console.WriteLine(lineString);
            }
        }

        /// <summary>
        /// Asks the user what game move they would like to make
        /// </summary>
        /// <param name="stockSize">The number of remaining cards in the stock</param>
        /// <returns>The user's response</returns>
        static string PromptUser(int stockSize)
        {
            bool answerValid = false;
            string answer = "";
            while (answerValid == false)
            {
                Console.WriteLine("M: Move a card");
                if (stockSize > 0)
                {

                    Console.WriteLine("D: Draw a card");
                }
                else
                {
                    Console.WriteLine("P: Put back cards");
                }
                answer = Console.ReadLine().ToLower();

                if (answer == "m" ^ (answer == "d" & stockSize > 0) ^ (answer == "p" & stockSize == 0))
                {
                    answerValid = true;
                }
            }
            if (answer == "m")
            {
                int pileNum = 0;
                answerValid = false;
                while (answerValid == false)
                {
                    Console.WriteLine("Press 1-7 to move a card from pile 1-7 or");
                    Console.WriteLine("Press D to move from the draw pile (if applicable)");
                    answer = Console.ReadLine().ToLower();

                   
                    bool intCheck = Int32.TryParse(answer, out pileNum);
                    if (intCheck & (pileNum > 0 & pileNum < 8))
                    {
                        answerValid = true;
                    }

                    if (answer == "d")
                    {
                        answerValid = true;
                        answer = "md";
                    }
                }
            }
            return answer;
        }

        /// <summary>
        /// Allows the user to pick up a card from the stock
        /// </summary>
        /// <param name="stock">The pile of cards that can be drawn from</param>
        /// <param name="waste">The pile of cards that have not been played</param>
        static void DrawCard(Pile stock, Pile waste)
        {
            if (waste.GetSize() > 0)
            {
                waste.GetLast().SetFacing(0);
            }
            Card card = stock.RemoveCard();
            card.SetFacing(1);
            waste.AddCard(card);
        }

        /// <summary>
        /// Returns the cards from the waste to the stock
        /// </summary>
        /// <param name="stock">A pile of cards</param>
        /// <param name="waste">The pile of cards that are to be moved</param>
        static void PutBack(Pile stock, Pile waste)
        {
            while (waste.GetSize() > 0)
            {
                Card card = waste.RemoveCard();
                stock.AddCard(card);
            }
        }

        /// <summary>
        /// Handles the validity of movement of cards
        /// </summary>
        /// <param name="from">Starting position of the card to be moved</param>
        /// <param name="tableau">The tableau</param>
        /// <param name="foundations">The list of foundations</param>
        /// <param name="waste">The waste pile</param>
        static void MoveCheck(string from, List<Pile> tableau, List<Pile> foundations, Pile waste)
        {
            int pileNum = 0;
            bool intCheck = Int32.TryParse(from, out pileNum);
            Card card = null;
            Pile pile = null;

            if (intCheck)
            {
                pile = tableau[pileNum - 1];
                int size = pile.GetSize();
                if (size > 1)
                {
                    if ((pile.GetCard(size - 2).GetFacing() == 1)) {
                        card = GetFromPile(pile);
                    }
                    else
                    {
                        card = pile.GetLast();
                    }
                } 
                else if (size > 0)
                {
                    card = pile.GetLast();
                }
                else
                {
                    Console.WriteLine("Invalid move");
                }
            } 
            else
            {
                pile = waste;
                card = pile.GetLast();
            }
 
            if (card != null)
            {
                string answer = "";
                bool answerValid = false;
                while (answerValid == false)
                {
                    Console.WriteLine("Press 1 - 7 to move the card to the chosen pile or");
                    Console.WriteLine("Press F to move card to its foundation");
                    answer = Console.ReadLine().ToLower();

                    pileNum = 0;
                    intCheck = Int32.TryParse(answer, out pileNum);
                    if ((intCheck & (pileNum > 0 & pileNum < 8)) ^ (answer == "f"))
                    {
                        answerValid = true;
                    }
                }

                Pile movePile = null;
                Card placeOn = null;
                if (intCheck)
                {
                    movePile = tableau[pileNum - 1];
                    int size = movePile.GetSize();
                    if (size > 0) 
                    {
                        placeOn = movePile.GetLast();
                    }
                }

                if (placeOn != null)
                {
                    if ((card.GetRankNo() == (placeOn.GetRankNo() - 1)) & (card.GetColour() != placeOn.GetColour()))
                    {
                        Move(card, pile, movePile);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move");
                    }
                }
                else if (answer == "f")
                {
                    string suit = card.GetSuit();
                    if (suit == "D")
                    {
                        movePile = foundations[0];
                    }
                    else if (suit == "H")
                    {
                        movePile = foundations[1];
                    }
                    else if (suit == "C")
                    {
                        movePile = foundations[2];
                    }
                    else
                    {
                        movePile = foundations[3];
                    }

                    if (movePile.GetSize() > 0)
                    {
                        if (card.GetRankNo() == (movePile.GetLast().GetRankNo() + 1))
                        {
                            Move(card, pile, movePile);
                        }
                    }
                    else if (movePile.GetSize() == 0 & card.GetRankNo() == 1)
                    {
                        Move(card, pile, movePile);
                    } 
                    else
                    {
                        Console.WriteLine("Invalid move");
                    }

                } 
                else
                {
                    if (card.GetRank() == "K")
                    {
                        Move(card, pile, movePile);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move");
                    }
                }
            }
        }

        /// <summary>
        /// Handles the movement of cards from one pile to another
        /// </summary>
        /// <param name="card">The first card to be moved</param>
        /// <param name="start">The initial pile</param>
        /// <param name="end">The pile to be moved to</param>
        static void Move(Card card, Pile start, Pile end)
        {
            Card current = start.GetLast();
            List<Card> moveList = new();
            while (start.GetLast().Display() != card.Display())
            {
                moveList.Add(current);
                start.RemoveCard();
                current = start.GetLast();
            }
            start.RemoveCard();
            moveList.Add(current);
            while (moveList.Count > 0)
            {
                current = moveList[moveList.Count - 1];
                end.AddCard(current);
                moveList.Remove(current);
            }
        }

        /// <summary>
        /// Allows the user to choose where in the pile to move cards from if multiple are visible
        /// </summary>
        /// <param name="pile">The pile that is being moved from</param>
        /// <returns>The top card of those that are being moved</returns>
        static Card GetFromPile(Pile pile)
        {
            List<Card> options = new();
            int showCount = 0;
            for (int i = 0; i < pile.GetSize(); i++)
            {
                Card card = pile.GetCard(i);
                if (card.GetFacing() == 1)
                {
                    showCount++;
                    string display = showCount.ToString() + ". " + card.GetRank() + card.GetSuit();
                    Console.WriteLine(display);
                    options.Add(card);
                }
            }
            bool answerValid = false;
            int ansNum = 0;
            while (answerValid == false)
            {
                Console.WriteLine("What card would you like to move");
                string answer = Console.ReadLine().ToLower();
                if (int.TryParse(answer, out ansNum) & ansNum > 0 & ansNum <= options.Count())
                {
                    answerValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid answer");
                }
            }
            return options[ansNum - 1];
        }

        /// <summary>
        /// Checks after every move whether the player has succeeded the conditions to win a match or not
        /// </summary>
        /// <param name="tableau">The tableau</param>
        /// <param name="stockSize">Number of remaining cards in the stock</param>
        /// <returns>If the user is still playing</returns>
        static bool WinCheck(List<Pile> tableau, int stockSize)
        {
            bool win = true;

            while (win)
            {
                foreach (Pile pile in tableau)
                {
                    for (int x = 0; x < pile.GetSize(); x++)
                    {
                        Card card = pile.GetCard(x);
                        if (card.GetFacing() == 0)
                        {
                            win = false;
                            break;
                        }
                    }
                }
            }

            return !win;
        }
    }
}
