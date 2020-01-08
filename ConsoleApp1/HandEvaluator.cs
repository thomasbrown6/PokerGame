using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Poker.Models.Card;

namespace Poker
{
    public enum Hand
    {
        StraightFlush,
        ThreeOfAKind,
        Straight,
        Flush,
        Pair,
        HighCard
    }

    public struct HandValue
    {
        public int Total { get; set; }
    }

    class HandEvaluator
    {
        public List<Player> GetWinner(List<Player> players)
        {
            List<Player> winningPlayers = new List<Player>();

            // STRAIGHT FLUSH
            winningPlayers = StraightFlush(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;

            // THREE OF A KIND
            winningPlayers = ThreeOfAKind(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;

            // STRAIGHT
            winningPlayers = Straight(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;

            // FLUSH
            winningPlayers = Flush(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;

            // PAIR
            winningPlayers = Pair(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;

            // HIGH CARD
            winningPlayers = HighCard(players);
            if (winningPlayers != null && winningPlayers.Count > 0)
                return winningPlayers;


            return winningPlayers;
        }
    

        private List<Player> StraightFlush(List<Player> players)
        {
            List<Player> straightFlushPlayers = new List<Player>();

            foreach (Player player in players)
            {
                if (IsFlush(player.cards))
                {
                    if (isStraight(player.cards))
                    {
                        straightFlushPlayers.Add(player);
                    }
                }
            }

            if (straightFlushPlayers != null && straightFlushPlayers.Count > 1)
            {
                straightFlushPlayers = HighCard(straightFlushPlayers);
            }

            return straightFlushPlayers;
        }


        private List<Player> ThreeOfAKind(List<Player> players)
        {
            List<Player> threeOfAKindPlayers = new List<Player>();

            foreach (Player player in players)
            {
               if (player.cards[0].Value == player.cards[1].Value && player.cards[1].Value == player.cards[2].Value)
                {
                    threeOfAKindPlayers.Add(player);
                }
            }

            if (threeOfAKindPlayers != null && threeOfAKindPlayers.Count > 1)
            {
                threeOfAKindPlayers = HighCard(threeOfAKindPlayers);
            }

            return threeOfAKindPlayers;
        }

        private List<Player> Straight(List<Player> players)
        {
            List<Player> straightPlayers = new List<Player>();

            foreach (Player player in players)
            {
                if (isStraight(player.cards))
                {
                    straightPlayers.Add(player);
                }
            }

            if (straightPlayers != null && straightPlayers.Count > 1)
            {
                straightPlayers = HighCard(straightPlayers);
            }

            return straightPlayers;
        }

        private List<Player> Flush(List<Player> players)
        {
            List<Player> flushPlayers = new List<Player>();

            foreach (Player player in players)
            {
                if (IsFlush(player.cards))
                {
                    flushPlayers.Add(player);
                }
            }

            if (flushPlayers != null && flushPlayers.Count > 1)
            {
                flushPlayers = HighCard(flushPlayers);
            }

            return flushPlayers;
        }

        private List<Player> Pair(List<Player> players)
        {
            List<Player> pairPlayers = new List<Player>();

            foreach (Player player in players)
            {
                // Check if we have a pair, save the pair to model for comparision later
                if (player.cards[0].Value == player.cards[1].Value)
                {
                    player.pair = player.cards[0].Value;
                    pairPlayers.Add(player);
                }

                if (player.cards[1].Value == player.cards[2].Value)
                {
                    player.pair = player.cards[1].Value;
                    pairPlayers.Add(player);
                }

                if (player.cards[0].Value == player.cards[2].Value)
                {
                    player.pair = player.cards[0].Value;
                    pairPlayers.Add(player);
                }
            }

            if (pairPlayers != null && pairPlayers.Count > 1)
            {
                pairPlayers = HighPair(pairPlayers);
            }

            return pairPlayers;
        }

        private List<Player> HighCard(List<Player> players)
        {
            List<Player> highCardPlayers = new List<Player>();

            // Check the highest card for all players
            for (int i = 14; i > 1; i--)
            {
                highCardPlayers = GetPlayersWithValue(players, i, 0);

                if (highCardPlayers != null)
                {
                    break;
                }
            }

            // If more than one player, check 2nd highest card
            if (highCardPlayers != null && highCardPlayers.Count > 1)
            {
                for (int i = 14; i > 1; i--)
                {
                    highCardPlayers = GetPlayersWithValue(players, i, 1);

                    if (highCardPlayers != null && highCardPlayers.Count > 0)
                    {
                        break;
                    }
                }
            }

            // If more than one player, check 3rd highest card
            if (highCardPlayers != null && highCardPlayers.Count > 1)
            {
                for (int i = 14; i > 1; i--)
                {
                    highCardPlayers = GetPlayersWithValue(players, i, 2);

                    if (highCardPlayers != null && highCardPlayers.Count > 0)
                    {
                        break;
                    }
                }
            }

            return highCardPlayers;
        }



        #region "Helper Methods"


        private List<Player> GetPlayersWithValue(List<Player> players, int evalueIndex, int cardIndex)
        {
            List<Player> valuePlayers = new List<Player>();
            foreach (Player player in players)
            {
                List<Card> SortedCards = player.cards.OrderByDescending(c => (int)c.Value).ToList();

                if (SortedCards[cardIndex].Value == (EVALUE)evalueIndex)
                {
                    valuePlayers.Add(player);
                }

            }

            return valuePlayers;
        }

        private bool IsFlush(List<Card> cards)
        {

            if (cards[0].Suit == cards[1].Suit && cards[1].Suit == cards[2].Suit)
                return true;
            else
                return false;
        }

        private bool isStraight(List<Card> cards)
        {

            List<Card> SortedCards = cards.OrderBy(c => (int)c.Value).ToList();

            // If values are in order
            if ((int)SortedCards[0].Value + 1 == (int)SortedCards[1].Value && (int)SortedCards[1].Value + 1 == (int)SortedCards[2].Value)
                return true;
            // If there's a 'A-2-3' run
            else if (SortedCards[2].Value == Card.EVALUE.A && SortedCards[0].Value == Card.EVALUE.TWO && SortedCards[0].Value == Card.EVALUE.THREE)
                return true;
            else
                return false;
           
        }

        private List<Player> HighPair(List<Player> players)
        {
            List<Player> highPairPlayers = new List<Player>();

            List<int> pairs = new List<int>();


            foreach (Player player in players)
            {
                pairs.Add((int)player.pair);
            }

            pairs.Sort();

            foreach (Player player in players)
            {
                if ((int)player.pair == pairs[0])
                {
                    highPairPlayers.Add(player);
                }
            }

            return highPairPlayers;
        }


        #endregion

    }
}
