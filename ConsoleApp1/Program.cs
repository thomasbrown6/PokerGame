using Poker;
using Poker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Poker.Models.Card;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            int numOfPlayers;
            List<Player> players = new List<Player>();


            // Error handling
            if (!int.TryParse(Console.ReadLine(), out numOfPlayers))
            {
                Console.WriteLine("Please enter the number of players that are playing.");
                return;
            } else if (numOfPlayers > 23)
            {
                Console.WriteLine("Number of players have to be less than 24");
                return;
            }


            // Save all the players and their cards
            int playercount = numOfPlayers;
            while(playercount > 0)
            {
                string[] cardInput = Console.ReadLine().Split(new char[0]);
                Player player = new Player();
                Int32.TryParse(cardInput[0], out player.Id);

                player.cards = new List<Card>();

                for (int i = 1; i < 4; i++)
                {
                    Card card = new Card();

                    char[] value = cardInput[i].ToCharArray();
                    card.Value = (EValue)Enum.Parse(typeof(EValue), value[0].ToString());
                    card.Suit = (ESuit)Enum.Parse(typeof(ESuit), value[1].ToString());

                    player.cards.Add(card);
                }


                players.Add(player);
                playercount--;
            }

            HandEvaluator handEvaluator = new HandEvaluator();

            List<Player> winningPlayers = handEvaluator.GetWinner(players);


            string winnnersId = string.Empty;

            foreach (Player winner in winningPlayers)
            {
                winnnersId += $" {winner.Id} ";
            }

            Console.WriteLine(Environment.NewLine);

            if (!string.IsNullOrEmpty(winnnersId) && winnnersId.Trim().Length > 1)
            {
                Console.WriteLine($"We have a tie! {winnnersId}");
            }
            else if (string.IsNullOrEmpty(winnnersId))
            {
                Console.WriteLine($"No Winners");
            }
            else
            {
                Console.WriteLine($"Winner: {winnnersId}");
            }

            if (winningPlayers.Count > 0)
            {
                Console.WriteLine($"Winning Hand Type: {winningPlayers[0].handType.ToString()}");
            }



            int playagain = 0;
            if (!int.TryParse(Console.ReadLine(), out playagain))
            {
                if (playagain == 1)
                    Main(null);
                else
                    Environment.Exit(0);

            }

        }
    }
}
