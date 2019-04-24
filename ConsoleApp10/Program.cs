using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace ConsoleApp3
{
    class Program
    {
        class Game
        {
            private Player p1 = new Player();
            private Player p2 = new Player();
            List<Card> cards = new List<Card>(36);

            public Game()
            {

                int index = 0;
                for (int i = 0; i < 4; i++)
                {
                    int ranker = 1;
                    for (int j = 0; j < 9; j++)
                    {
                        cards.Add(new Card(ranker));
                        ranker++;
                    }
                }
                index = 0;
                string suit = "Hearts";
                for (int j = 0; j < 9; j++)
                {
                    cards[index].Suit = suit;
                    index++;
                }
                suit = "Spades";
                for (int j = 0; j < 9; j++)
                {
                    cards[index].Suit = suit;
                    index++;
                }
                suit = "Clubs";
                for (int j = 0; j < 9; j++)
                {
                    cards[index].Suit = suit;
                    index++;
                }
                suit = "Diamonds";
                for (int j = 0; j < 9; j++)
                {
                    cards[index].Suit = suit;
                    index++;
                }
            }
            public void ShowCards()
            {
                WriteLine("ALL CARDS:");
                for (int i = 0; i < cards.Count; i++)
                {
                    WriteLine(cards[i].Rank + "-" + cards[i].Suit);
                }
            }
            public void Shuffle()
            {
                Random rand = new Random();
                Card tmp = new Card();

                for (int n = 0; n < 36; n++)
                {
                    cards[n].Index = rand.Next(0, 1000);
                }
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    for (int j = 0; j < cards.Count - i - 1; j++)
                    {
                        if (cards[j].Index > cards[j + 1].Index)
                        {
                            tmp = cards[j];
                            cards[j] = cards[j + 1];
                            cards[j + 1] = tmp;
                        }
                    }
                }
            }
            public void GiveCardToPlayers()
            {
                int j = 0;
                for (int i = 0; i < 18; i++)
                {
                    p1.GetCard(cards[j]);
                    j++;
                    p2.GetCard(cards[j]);
                    j++;

                }
            }
            public void ShowPlayersCard()
            {
                WriteLine("\n\nPlayer 1: " + p1.CountOfCard() + "\n");
                p1.ShowCards();
                WriteLine("\n\nPlayer 2: " + p2.CountOfCard() + "\n");
                p2.ShowCards();
            }
            public void MakeStep()
            {
                Card card1 = p1.GetLowerCard();
                Card card2 = p2.GetLowerCard();

                if (card2.Rank > card1.Rank)
                {
                    p2.AddToTail(card1);
                    p2.AddToTail(card2);
                    p1.Remove();
                    p2.Remove();

                }
                else
                {
                    p1.AddToTail(card1);
                    p1.AddToTail(card2);
                    p1.Remove();
                    p2.Remove();

                }
            }

        }
        class Card
        {
            public string Suit { get; set; }
            public int Rank { get; set; }
            public int Index { get; set; } // для перетасовки

            public Card() { }
            public Card(int rank)
            {
                Rank = rank;
            }
            public Card(string suit, int rank)
            {
                Suit = suit;
                Rank = rank;
            }
        }
        class Player
        {
            List<Card> cards = new List<Card>();

            public Player() { }


            public Card GetUpperCard()
            {
                return cards[cards.Count - 1];
            }
            public Card GetLowerCard()
            {
                return cards[0];
            }
            public void Remove()
            {
                cards.RemoveAt(0);
            }
            public void AddToHead(Card card)
            {
                List<Card> new_cards = new List<Card>();
                new_cards.Add(card);
                for (int i = 0; i < cards.Count; i++)
                {
                    new_cards.Add(cards[i]);
                }
                cards = new_cards;
            }
            public void AddToTail(Card card)
            {
                cards.Add(card);
            }
            public void ShowCards()
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    WriteLine(cards[i].Rank + "-" + cards[i].Suit);
                }
            }
            public void GetCard(Card card)
            {
                cards.Add(card);
            }
            public int CountOfCard()
            {
                return cards.Count;
            }
        }

        static void Main(string[] args)
        {
            Game game = new Game();


            game.Shuffle();
            //game.ShowCards();
            game.GiveCardToPlayers();

            game.ShowPlayersCard();

            while (true)
            {
                game.MakeStep();
                game.ShowPlayersCard();

                ReadKey();
                Clear();
            }
        }
    }
}
