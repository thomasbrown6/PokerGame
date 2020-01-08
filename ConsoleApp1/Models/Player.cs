using System;
using System.Collections.Generic;
using System.Text;
using static Poker.Models.Card;

namespace Poker.Models
{
    class Player
    {
        public int Id;
        public List<Card> cards;
        public EVALUE pair;
    }
}
