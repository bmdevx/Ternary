﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ternary.Old
{
    [DebuggerDisplay("{DebuggerInfo}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Tryte : IEnumerable<Trit>, IComparable, IComparable<Tryte>, IEquatable<Tryte>, IFormattable//, IConvertible
    {
        public const int NUMBER_OF_TRITS = 9;

        public const int MAX_INT_VALUE = 9841;
        public const int MIN_INT_VALUE = -9841;

        public static readonly Tryte MAX_VALUE = new Tryte(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);
        public static readonly Tryte MIN_VALUE = new Tryte(Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg);
        
        internal string DebuggerInfo => $"{ToString()} ({ToString("s")})";

        public Trit[] Trits => new Trit[] { T0, T1, T2, T3, T4, T5, T6, T7, T8 };

        public Trit[] LowerTribble => new Trit[] { T0, T1, T2 };
        public Trit[] MiddleTribble => new Trit[] { T3, T4, T5 };
        public Trit[] UpperTribble => new Trit[] { T6, T7, T8 };

        public int LowerTribbleValue => ToInt(new Trit[] { T0, T1, T2 });
        public int MiddleTribbleValue => ToInt(new Trit[] { T3, T4, T5 });
        public int UpperTribbleValue => ToInt(new Trit[] { T6, T7, T8 });


        public Trit T0;
        public Trit T1;
        public Trit T2;
        public Trit T3;
        public Trit T4;
        public Trit T5;
        public Trit T6;
        public Trit T7;
        public Trit T8;


        public Tryte(Trit t0 = Trit.Neu, Trit t1 = Trit.Neu, Trit t2 = Trit.Neu,
            Trit t3 = Trit.Neu, Trit t4 = Trit.Neu, Trit t5 = Trit.Neu,
            Trit t6 = Trit.Neu, Trit t7 = Trit.Neu, Trit t8 = Trit.Neu)
        {
            T0 = t0;
            T1 = t1;
            T2 = t2;
            T3 = t3;
            T4 = t4;
            T5 = t5;
            T6 = t6;
            T7 = t7;
            T8 = t8;
        }

        public Tryte(IEnumerable<Trit> trits)
        {
            this = new Tryte();

            int i = 0;
            foreach (Trit t in trits)
            {
                switch (i)
                {
                    case 0: T0 = t; break;
                    case 1: T1 = t; break;
                    case 2: T2 = t; break;
                    case 3: T3 = t; break;
                    case 4: T4 = t; break;
                    case 5: T5 = t; break;
                    case 6: T6 = t; break;
                    case 7: T7 = t; break;
                    case 8: T8 = t; break;
                }

                i++;
            }
        }

        public Tryte(int value)
        {
            if (value >= MAX_INT_VALUE)
                this = MAX_VALUE;
            else if (value <= MIN_INT_VALUE)
                this = MIN_VALUE;
            else
            {
                Trit[] trits = new Trit[Tryte.NUMBER_OF_TRITS];
                for (int i = 0; value != 0 && i < NUMBER_OF_TRITS; i++)
                {
                    switch (value % 3)
                    {
                        case -1:
                        case 2: trits[i] = Trit.Neg; break;
                        case -2:
                        case 1: trits[i] = Trit.Pos; break;
                        default: trits[i] = Trit.Neu; break;
                    }

                    value = (value < 0 ? (value - 1) : (value + 1)) / 3;
                }

                T0 = trits[0];
                T1 = trits[1];
                T2 = trits[2];
                T3 = trits[3];
                T4 = trits[4];
                T5 = trits[5];
                T6= trits[6];
                T7 = trits[7];
                T8 = trits[8];
            }
        }


        public IEnumerator<Trit> GetEnumerator()
        {
            yield return T0;
            yield return T1;
            yield return T2;
            yield return T3;
            yield return T4;
            yield return T5;
            yield return T6;
            yield return T7;
            yield return T8;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public int CompareTo(object obj)
        {
            return (obj is Tryte tryte) ? CompareTo(tryte) : -1;
        }

        public int CompareTo(Tryte other)
        {
            for (int i = NUMBER_OF_TRITS - 1; i > -1; i--)
            {
                if (this[i] != other[i])
                    return this[i].Value();
            }

            return 0;
        }


        public override bool Equals(object obj)
        {
            return (obj is Tryte tryte) ? Equals(tryte) : false;
        }

        public bool Equals(Tryte other)
        {
            return this.SequenceEqual(other);
        }


        public override int GetHashCode()
        {
            var hashCode = -878873857;
            hashCode = hashCode * -1521134295 + T0.GetHashCode();
            hashCode = hashCode * -1521134295 + T1.GetHashCode();
            hashCode = hashCode * -1521134295 + T2.GetHashCode();
            hashCode = hashCode * -1521134295 + T3.GetHashCode();
            hashCode = hashCode * -1521134295 + T4.GetHashCode();
            hashCode = hashCode * -1521134295 + T5.GetHashCode();
            hashCode = hashCode * -1521134295 + T6.GetHashCode();
            hashCode = hashCode * -1521134295 + T7.GetHashCode();
            hashCode = hashCode * -1521134295 + T8.GetHashCode();
            return hashCode;
        }


        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            switch (format)
            {
                case "s":
                case "S": return new string(this.Select(t => t.ToSymbol()).ToArray());
            }

            return ToString();
        }

        public override string ToString()
        {
            return ToInt().ToString();
        }


        public Trit this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return T0;
                    case 1: return T1;
                    case 2: return T2;
                    case 3: return T3;
                    case 4: return T4;
                    case 5: return T5;
                    case 6: return T6;
                    case 7: return T7;
                    case 8: return T8;
                }

                throw new IndexOutOfRangeException();
            }

            set
            {
                switch (index)
                {
                    case 0: T0 = value; break;
                    case 1: T1 = value; break;
                    case 2: T2 = value; break;
                    case 3: T3 = value; break;
                    case 4: T4 = value; break;
                    case 5: T5 = value; break;
                    case 6: T6 = value; break;
                    case 7: T7 = value; break;
                    case 8: T8 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }


        public int ToInt()
        {
            return ToInt(this);
        }

        public static int ToInt(IEnumerable<Trit> trits)
        {
            int s = 0;

            int m = 1;
            foreach (Trit t in trits)
            {
                s += m * t.Value();
                m *= 3;
            }

            return s;
        }

        public char ToChar()
        {
            int val = ToInt();
            return (char)(val < 0 ? val + MAX_INT_VALUE : val);
        }


        private Tryte Invert()
        {
            return new Tryte(
                T0.Invert(),
                T1.Invert(),
                T2.Invert(),
                T3.Invert(),
                T4.Invert(),
                T5.Invert(),
                T6.Invert(),
                T7.Invert(),
                T8.Invert());
        }


        public static Tryte Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            Trit[] trits = new Trit[NUMBER_OF_TRITS];

            for (int i = 0; i < NUMBER_OF_TRITS && i < value.Length; i++)
            {
                if (TritEx.TryParse(value[i], out Trit trit))
                    trits[i] = trit;
                else
                    throw new FormatException();
            }

            return new Tryte(trits);
        }

        public static bool TryParse(string value, out Tryte tryte)
        {
            tryte = new Tryte();

            if (value == null)
                return false;

            if (Int32.TryParse(value, out Int32 s))
            {
                if (s < MIN_INT_VALUE || s > MAX_INT_VALUE)
                    return false;

                tryte = new Tryte(s);
            }
            else
            {
                for (int i = 0; i < NUMBER_OF_TRITS && i < value.Length; i++)
                {
                    if (TritEx.TryParse(value[i], out Trit trit))
                        tryte[i] = trit;
                    else
                        return false;
                } 
            }

            return true;
        }


        #region Overloads

        public static implicit operator Tryte(int value)
        {
            return new Tryte(value);
        }


        public static Tryte operator +(Tryte t1, Tryte t2)
        {
            Tryte tryte = new Tryte();
            Trit carry = Trit.Neu;
            for (int i = 0; i < NUMBER_OF_TRITS; i++)
            {
                tryte[i] = t1[i].Add(t2[i], ref carry);

                while (carry != Trit.Neu)
                {
                    if (++i == NUMBER_OF_TRITS)
                        break;

                    Trit tc = Trit.Neu;
                    tryte[i] = carry.Add(t1[i], ref tc).Add(t2[i], ref carry);

                    if (carry == Trit.Neu)
                        carry = tc;
                    else if (tc != Trit.Neu)
                        carry = carry.Add(tc, ref tc);
                }
            }

            return tryte;
        }

        public static Tryte operator +(Tryte tryte, int val)
        {
            return tryte.ToInt() + new Tryte(val);
        }

        public static int operator +(int val, Tryte tryte)
        {
            return val + tryte.ToInt();
        }


        public static Tryte operator -(Tryte t1, Tryte t2)
        {
            return t1 + t2.Invert();
        }

        public static Tryte operator -(Tryte tryte, int val)
        {
            return tryte - new Tryte(val);
        }

        public static int operator -(int val, Tryte tryte)
        {
            return val - tryte.ToInt();
        }


        public static Tryte operator *(Tryte t1, Tryte t2)
        {
            Tryte tryte = new Tryte();

            int stop = t2.ToInt();
            bool neg = false;

            if (stop < 0)
            {
                stop = -stop;
                neg = true;
            }

            for (int i = 0; i < stop; i++)
            {
                tryte += t1;
            }

            return neg ? -tryte : tryte;
        }

        public static Tryte operator /(Tryte t1, Tryte t2)
        {
            Tryte tmp = t1, val = new Tryte();
            
            if (t1 < 0)
            {
                while ((tmp += t2) <= 0)
                    val--;
            }
            else
            {
                while ((tmp -= t2) >= 0)
                    val++;
            }

            return val;
        }

        public static Tryte operator %(Tryte t1, Tryte t2)
        {
            Tryte tmp = t1;

            if (t1 < 0)
            {
                while ((tmp += t2) <= t2) ;
            }
            else
            {
                while ((tmp -= t2) >= t2) ;
            }

            return tmp;
        }


        public static Tryte operator ++(Tryte tryte)
        {
            return tryte + new Tryte(Trit.Pos);
        }

        public static Tryte operator --(Tryte tryte)
        {
            return tryte + new Tryte(Trit.Neg);
        }

        public static Tryte operator -(Tryte tryte)
        {
            return tryte.Invert();
        }

        public static Tryte operator ~(Tryte tryte)
        {
            return tryte.Invert();
        }


        public static bool operator ==(Tryte t1, Tryte t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Tryte t1, Tryte t2)
        {
            return !t1.Equals(t2);
        }

        public static bool operator >(Tryte t1, Tryte t2)
        {
            for (int i = 5; i > -1; i--)
            {
                if (t1[i] > t2[i])
                    return true;
                else if (t1[i] < t2[i])
                    return false;
            }

            return false;
        }

        public static bool operator <(Tryte t1, Tryte t2)
        {
            for (int i = 5; i > -1; i--)
            {
                if (t1[i] < t2[i])
                    return true;
                else if (t1[i] > t2[i])
                    return false;
            }

            return false;
        }

        public static bool operator >=(Tryte t1, Tryte t2)
        {
            return t1 == t2 || t1 > t2;
        }

        public static bool operator <=(Tryte t1, Tryte t2)
        {
            return t1 == t2 || t1 < t2;
        }
        #endregion
    }

    public static class TryteEx
    {
        public static Tryte ToTryte(this IEnumerable<Trit> trits)
        {
            return new Tryte(trits);
        }
    }
}
