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

        public static Trit Parse(char input)
        {
            switch (input)
            {
                case '-': return Trit.Neg;
                case '0': return Trit.Neu;
                case '+':
                case '1': return Trit.Pos;
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

        public static bool TryParse(char input, out Trit trit)
        {
            switch (input)
            {
                case '-': trit = Trit.Neg; break;
                case '0': trit = Trit.Neu; break;
                case '+':
                case '1': trit = Trit.Pos; break;
                default: trit = Trit.Neu; return false;
            }

            return true;
        }

        public static sbyte Value(this Trit trit)
        {
            if (trit == Trit.Neg)
                return -1;
            else if (trit == Trit.Pos)
                return 1;
            return 0;
        }

        public static char ToSymbol(this Trit trit)
        {
            if (trit == Trit.Neg)
                return '-';
            else if (trit == Trit.Pos)
                return '+';
            return '0';
        }
    }
}
