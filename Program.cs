using System;
using System.Collections.Generic;

namespace hackathon_war
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Adds {player1.Name}
            Console.WriteLine("Enter your player1 name: ");
            string name = Console.ReadLine();
            Player player1 = new Player(name);
            //Adds player 2
            Console.WriteLine("Enter your player2 name: "); 
            string name1 = Console.ReadLine(); 
            Player player2 = new Player(name1);
            //Creates Game
            War newGame = new War(player1, player2);
            //Starts Game
            Console.WriteLine("Hit any key to play a round!: "); 
            Console.ReadKey();
            do
            {
                while (!Console.KeyAvailable) {
                    //Do something
                    new Round(player1, player2);
                    System.Console.WriteLine("***********************************************");
                    break;
                    }
                if(player1.Hand.Count == 52)
                {
                    System.Console.WriteLine("Congratulations " + name);
                }
                else if(player2.Hand.Count == 52)
                {
                    System.Console.WriteLine($"Congratulations {name1}");
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }
    public class Card
    {
        public string[] cardValue = {"Ace","2","3","4","5","6","7","8","9","10","Jack","Queen","King"};
        public string[] cardSuit = {"Clubs", "Spades", "Hearts", "Diamonds"};
        public int[] cardNumVal = {1,2,3,4,5,6,7,8,9,10,11,12,13};
        public string stringVal;
        public string Suit;
        public int Val;
        public Card(int stringval, int suit, int val)
        {
            stringVal = cardValue[stringval];
            Suit = cardSuit[suit];
            Val = cardNumVal[val];
        }
    }
    public class Deck
    {
        public List<Card> Cards;
        public Deck()
        {
            Cards = new List<Card>();
            {
                for(int x = 0; x < 4; x++)
                {
                    for(int y = 0; y < 13; y++)
                    {
                        Cards.Add(new Card(y,x,y));
                    }
                }
            };
        }
        public List<Card> Shuffle
        {
            get
            {
                for(int x = 0; x < Cards.Count; x++)
                {
                    Random rand = new Random();
                    int index = rand.Next(0,Cards.Count);
                    Card temp = Cards[x];
                    Cards[x] = Cards[index];
                    Cards[index] = temp;
                }
                return Cards;
            }
        }
        public Card Deal()
        {
            Card temp = Cards[0];
            Cards.RemoveAt(0);
            return temp;
        }
        public List<Card> Reset()
        {
            Cards = new Deck().Cards;
            return Cards;
        }
    }
    public class Player
    {
        public string Name;
        public List<Card> Hand;
        public List<Card> Winnings;
        public static int playerCount = 0;

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Winnings = new List<Card>();
            playerCount+=1;
        }
        public List<Card> Draw(Deck newCard)
        {
            Hand.Add(newCard.Deal());
            return Hand;
        }
        public void Discard(int index)
        {
            Card temp = Hand[index];
            Hand.Remove(Hand[index]);
        }
    }
    public class War
    {
        public War(Player player1, Player player2)
        {
            if(Player.playerCount == 2)
            {
                Deck newDeck = new Deck();
                object shuffled = newDeck.Shuffle;
                int cardCount = newDeck.Cards.Count;
                for(int x = 0; x < cardCount; x++)
                {
                    if(x%2 != 0)
                    {
                        player1.Draw(newDeck);
                    }
                    else
                    {
                        player2.Draw(newDeck);
                    }
                }
            }
            else
            {
                System.Console.WriteLine("You must have 2 players to play!");
            }
        }
        
    }

    public class Round
    {
        public Round(Player player1, Player player2)
        {
            if(player1.Hand.Count == 0)
            {
                if(player1.Winnings.Count == 0)
                {
                    System.Console.WriteLine($"{player1.Name} Lost!");
                }
                else
                {
                    Shuffle(player1);
                    player1.Hand = player1.Winnings;
                    player1.Winnings = new List<Card>();
                }
            }
            if (player2.Hand.Count == 0)
            {
                if (player2.Winnings.Count == 0)
                {
                    System.Console.WriteLine($"{player2.Name} Lost!");
                }
                else
                {
                    Shuffle(player2);
                    player2.Hand = player2.Winnings;
                    player2.Winnings = new List<Card>();
                    //winnings shuffle
                }
            }
            else
            {
                if(player1.Hand[0].Val > player2.Hand[0].Val)
                {
                System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}): {player1.Hand[0].stringVal} of {player1.Hand[0].Suit}");
                System.Console.WriteLine($"{player2.Name}({player2.Hand.Count})('Winnings:'{player2.Winnings.Count}): {player2.Hand[0].stringVal} of {player2.Hand[0].Suit}");
                    player1.Winnings.Add(player1.Hand[0]);
                    player1.Winnings.Add(player2.Hand[0]);
                    player1.Hand.RemoveAt(0);
                    player2.Hand.RemoveAt(0);
                    System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}) won the round!");
                }
                else if(player1.Hand[0].Val == player2.Hand[0].Val)
                {
                    System.Console.WriteLine("It's a tie! Time for WAR!!!");
                    cardsEqual(player1, player2); 
                }
                else if (player1.Hand[0].Val < player2.Hand[0].Val)
                {
                    System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}): {player1.Hand[0].stringVal} of {player1.Hand[0].Suit}");
                    System.Console.WriteLine($"{player2.Name}({player2.Hand.Count})('Winnings:'{player2.Winnings.Count}): {player2.Hand[0].stringVal} of {player2.Hand[0].Suit}");
                    player2.Winnings.Add(player2.Hand[0]);
                    player2.Winnings.Add(player1.Hand[0]);
                    player1.Hand.RemoveAt(0);
                    player2.Hand.RemoveAt(0);
                    System.Console.WriteLine($"{player2.Name}({player2.Hand.Count}) won the round!");
                }
            }

        }
        public void cardsEqual(Player player1, Player player2)
        {
            int x = 0;
            System.Console.WriteLine("player1 total cards " + player1.Winnings.Count + player1.Hand.Count);
            System.Console.WriteLine("player2 total cards" + player2.Winnings.Count + player2.Hand.Count);
            while(player1.Hand[x].Val == player2.Hand[x].Val)
            {
                if(player1.Hand.Count < x){
                    System.Console.WriteLine(player1.Winnings.Count + player1.Hand.Count);
                    if((player1.Winnings.Count + player1.Hand.Count) < x){
                        System.Console.WriteLine($"{player1.Name}ran out of cards! LOSER");
                    }
                    else
                    {
                        Shuffle(player1);
                        int i = 0;
                        while(player1.Winnings.Count != 0){
                            player1.Hand.Add(player1.Winnings[i]);
                            i++;
                        }
                        player1.Winnings = new List<Card>();
                        System.Console.WriteLine("it works...");
                    }
                }
                if(player2.Hand.Count < x){
                    if((player2.Winnings.Count + player2.Hand.Count) < x){
                        System.Console.WriteLine($"{player2.Name}ran out of cards! LOSER");
                    }
                    else
                    {
                        Shuffle(player2);
                        int i = 0;
                        while(player2.Winnings.Count != 0){
                            player2.Hand.Add(player2.Winnings[i]);
                            i++;
                        }
                        player2.Winnings = new List<Card>();
                        System.Console.WriteLine("it works...");
                    }
                }
                System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}): {player1.Hand[x].stringVal} of {player1.Hand[x].Suit}");
                System.Console.WriteLine($"{player2.Name}({player2.Hand.Count})('Winnings:'{player2.Winnings.Count}): {player2.Hand[x].stringVal} of {player2.Hand[x].Suit}");
                System.Console.WriteLine("Holy crap another WAR!!!!");
                x += 4;
            }
            if(player1.Hand[x].Val > player2.Hand[x].Val)
            {
                System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}): {player1.Hand[x].stringVal} of {player1.Hand[x].Suit}");
                System.Console.WriteLine($"{player2.Name}({player2.Hand.Count})('Winnings:'{player2.Winnings.Count}): {player2.Hand[x].stringVal} of {player2.Hand[x].Suit}");
                System.Console.WriteLine($"{player1.Name} won the war!");
                for(int i = 0; i < x; i++)
                {
                    player1.Winnings.Add(player1.Hand[i]);
                    player1.Winnings.Add(player2.Hand[i]);
                    player1.Hand.RemoveAt(i);
                    player2.Hand.RemoveAt(i);
                }
            }
            else if (player1.Hand[x].Val < player2.Hand[x].Val)
            {
                System.Console.WriteLine($"{player1.Name}({player1.Hand.Count})('Winnings:'{player1.Winnings.Count}): {player1.Hand[x].stringVal} of {player1.Hand[x].Suit}");
                System.Console.WriteLine($"{player2.Name}({player2.Hand.Count})('Winnings:'{player2.Winnings.Count}): {player2.Hand[x].stringVal} of {player2.Hand[x].Suit}");
                System.Console.WriteLine($"{player2.Name} won the round!");
                for (int i = 0; i < x; i++)
                {
                    player2.Winnings.Add(player2.Hand[i]);
                    player2.Winnings.Add(player1.Hand[i]);
                    player1.Hand.RemoveAt(i);
                    player2.Hand.RemoveAt(i);
                }
            }
            if(player2.Hand.Count == 0)
            {
                Shuffle(player2);
                player2.Hand = player2.Winnings;
                player2.Winnings = new List<Card>();
                //winnings shuffle
            }
            if (player1.Hand.Count == 0)
            {
                Shuffle(player1);
                player1.Hand = player1.Winnings;
                player1.Winnings = new List<Card>();
                //winnings shuffle
            }
        }
        // public string noCards(Player player)
        // {
        //     if(player.Hand.Count < )
        // }
        public void Shuffle(Player player)
        {
            for(int x = 0; x < player.Winnings.Count; x++)
            {
                Random rand = new Random();
                int index = rand.Next(0,player.Winnings.Count);
                Card temp = player.Winnings[x];
                player.Winnings[x] = player.Winnings[index];
                player.Winnings[index] = temp;
            }
        }
    }
}
}