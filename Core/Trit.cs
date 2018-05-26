using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ternary
{
    public enum Trit
    {
        [Description("Negative")]
        Neg = -1,
        [Description("Neutral")]
        Neu = 0,
        [Description("Positive")]
        Pos = 1
    }

    public static class TritTools
    {
        public static Trit Invert(this Trit trit)
        {
            if (trit == Trit.Neg)
                return Trit.Pos;
            else if (trit == Trit.Pos)
                return Trit.Neg;

            return trit;
        }

        public static Trit Parse(string input)
        {
            switch (input.ToLower())
            {
                case "-":
                case "-1":
                case "neg":
                case "negative": return Trit.Neg;
                case "0":
                case "neu":
                case "neutral": return Trit.Neu;
                case "+":
                case "1":
                case "pos":
                case "positive": return Trit.Neg;
            }

            throw new Exception("Invalid Input");
        }

        public static bool TryParse(string input, out Trit trit)
        {
            switch (input.ToLower())
            {
                case "-":
                case "-1":
                case "neg":
                case "negative": trit = Trit.Neg; break;
                case "0":
                case "neu":
                case "neutral": trit = Trit.Neu; break;
                case "+":
                case "1":
                case "pos":
                case "positive": trit = Trit.Pos; break;
                default: trit = Trit.Neu; return false;
            }

            return true;
        }
    }
}
