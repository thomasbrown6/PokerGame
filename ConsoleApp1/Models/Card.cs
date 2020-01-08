using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Models
{
    class Card
    {
        public enum ESUIT
        { 
            h,
            s,
            d,
            c
        }

        public enum EVALUE
        {
            TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7,
            EIGHT = 8, NINE = 9, T = 10, J = 11, Q = 12, K = 13, A = 14
        }

        public ESUIT Suit { get; set; }
        public EVALUE Value { get; set; }
    }
}
