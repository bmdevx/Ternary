using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ternary
{
    [DebuggerDisplay("{DebuggerInfo}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Trort : ITernaryDataType, IComparable<Trort>, IEquatable<Trort>
    {
        public const int NUMBER_OF_TRITS = 12;
        int ITernaryDataType.NUMBER_OF_TRITS => NUMBER_OF_TRITS;

        public const int MAX_INT_VALUE = 265720;
        public const int MIN_INT_VALUE = -265720;
        public const int TOTAL_VALUE = 531441;

        public static readonly Trort MAX_VALUE =
            new Trort(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);
        public static readonly Trort MIN_VALUE =
            new Trort(Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg);
        
        public string DebuggerInfo => $"{ToString()} ({ToString("s")})";

        public Trit[] Trits => new Trit[] { T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11 };

        public Tryte LowerTryte => new Tryte(T0, T1, T2, T3, T4, T5);
        public Tryte UpperTryte => new Tryte(T6, T7, T8, T9, T10, T11);

        public int LowerTryteValue => ToInt(new Trit[] { T0, T1, T2, T3, T4, T5 });
        public int UpperTryteValue => ToInt(new Trit[] { T6, T7, T8, T9, T10, T11 });


        public Trit T0;
        public Trit T1;
        public Trit T2;
        public Trit T3;
        public Trit T4;
        public Trit T5;
        public Trit T6;
        public Trit T7;
        public Trit T8;
        public Trit T9;
        public Trit T10;
        public Trit T11;


        public Trort(Trit t0 = Trit.Neu, Trit t1 = Trit.Neu, Trit t2 = Trit.Neu,
            Trit t3 = Trit.Neu, Trit t4 = Trit.Neu, Trit t5 = Trit.Neu,
            Trit t6 = Trit.Neu, Trit t7 = Trit.Neu, Trit t8 = Trit.Neu,
            Trit t9 = Trit.Neu, Trit t10 = Trit.Neu, Trit t11 = Trit.Neu)
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
            T9 = t9;
            T10 = t10;
            T11 = t11;
        }

        public Trort(IEnumerable<Trit> trits)
        {
            this = new Trort();

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
                    case 9: T9 = t; break;
                    case 10: T10 = t; break;
                    case 11: T11 = t; break;
                }

                i++;
            }
        }

        public Trort(int value)
        {
            if (value >= MAX_INT_VALUE)
                this = MAX_VALUE;
            else if (value <= MIN_INT_VALUE)
                this = MIN_VALUE;
            else
            {
                Trit[] trits = new Trit[Trort.NUMBER_OF_TRITS];
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
                T6 = trits[6];
                T7 = trits[7];
                T8 = trits[8];
                T9 = trits[9];
                T10 = trits[10];
                T11 = trits[11];
            }
        }

        public Trort(Tryte lower, Tryte upper)
        {
            T0 = lower.T0;
            T1 = lower.T1;
            T2 = lower.T2;
            T3 = lower.T3;
            T4 = lower.T4;
            T5 = lower.T5;
            T6 = upper.T0;
            T7 = upper.T1;
            T8 = upper.T2;
            T9 = upper.T3;
            T10 = upper.T4;
            T11 = upper.T5;
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
            yield return T9;
            yield return T10;
            yield return T11;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public int CompareTo(object obj)
        {
            return (obj is Trort trort) ? CompareTo(trort) : -1;
        }

        public int CompareTo(Trort other)
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
            return (obj is Trort trort) ? Equals(trort) : false;
        }

        public bool Equals(Trort other)
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
            hashCode = hashCode * -1521134295 + T9.GetHashCode();
            hashCode = hashCode * -1521134295 + T10.GetHashCode();
            hashCode = hashCode * -1521134295 + T11.GetHashCode();
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
                    case 9: return T9;
                    case 10: return T10;
                    case 11: return T11;
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
                    case 9: T9 = value; break;
                    case 10: T10 = value; break;
                    case 11: T11 = value; break;
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


        private Trort Invert()
        {
            return new Trort(
                T0.Invert(),
                T1.Invert(),
                T2.Invert(),
                T3.Invert(),
                T4.Invert(),
                T5.Invert(),
                T6.Invert(),
                T7.Invert(),
                T8.Invert(),
                T9.Invert(),
                T10.Invert(),
                T11.Invert());
        }


        public static Trort Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();
            else if (TryParse(value, out Trort trort))
                return trort;
            else
                throw new FormatException("Invallid Tryte Format");
        }

        public static bool TryParse(string value, out Trort trort)
        {
            trort = new Trort();

            if (value == null)
                return false;

            if (value.Any(c => c > 49 && c < 58) && Int32.TryParse(value, out Int32 s))
            {
                if (s < MIN_INT_VALUE || s > MAX_INT_VALUE)
                    return false;

                trort = new Trort(s);
            }
            else
            {
                for (int i = 0; i < NUMBER_OF_TRITS && i < value.Length; i++)
                {
                    if (TritEx.TryParse(value[i], out Trit trit))
                        trort[i] = trit;
                    else
                        return false;
                }
            }

            return true;
        }

        ITernaryDataType ITernaryDataType.CreateFromTrits(IEnumerable<Trit> trits)
        {
            return new Trort(trits);
        }


        #region Overloads

        public static implicit operator Trort(int value)
        {
            return new Trort(value);
        }


        public static Trort operator +(Trort t1, Trort t2)
        {
            Trort trort = new Trort();
            Trit carry = Trit.Neu;
            for (int i = 0; i < NUMBER_OF_TRITS; i++)
            {
                trort[i] = t1[i].Add(t2[i], ref carry);

                while (carry != Trit.Neu)
                {
                    if (++i == NUMBER_OF_TRITS)
                        break;

                    Trit tc = Trit.Neu;
                    trort[i] = carry.Add(t1[i], ref tc).Add(t2[i], ref carry);

                    if (carry == Trit.Neu)
                        carry = tc;
                    else if (tc != Trit.Neu)
                        carry = carry.Add(tc, ref tc);
                }
            }

            return trort;
        }

        public static Trort operator +(Trort trort, int val)
        {
            return trort.ToInt() + new Trort(val);
        }

        public static int operator +(int val, Trort trort)
        {
            return val + trort.ToInt();
        }


        public static Trort operator -(Trort t1, Trort t2)
        {
            return t1 + t2.Invert();
        }

        public static Trort operator -(Trort trort, int val)
        {
            return trort - new Trort(val);
        }

        public static int operator -(int val, Trort trort)
        {
            return val - trort.ToInt();
        }


        public static Trort operator *(Trort t1, Trort t2)
        {
            Trort trort = new Trort();

            int stop = t2.ToInt();
            bool neg = false;

            if (stop < 0)
            {
                stop = -stop;
                neg = true;
            }

            for (int i = 0; i < stop; i++)
            {
                trort += t1;
            }

            return neg ? -trort : trort;
        }

        public static Trort operator /(Trort t1, Trort t2)
        {
            Trort tmp = t1, val = new Trort();
            
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

        public static Trort operator %(Trort t1, Trort t2)
        {
            Trort tmp = t1;

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


        public static Trort operator ++(Trort trort)
        {
            return trort + new Trort(Trit.Pos);
        }

        public static Trort operator --(Trort trort)
        {
            return trort + new Trort(Trit.Neg);
        }

        public static Trort operator -(Trort trort)
        {
            return trort.Invert();
        }

        public static Trort operator ~(Trort trort)
        {
            return trort.Invert();
        }


        public static bool operator ==(Trort t1, Trort t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Trort t1, Trort t2)
        {
            return !t1.Equals(t2);
        }

        public static bool operator >(Trort t1, Trort t2)
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

        public static bool operator <(Trort t1, Trort t2)
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

        public static bool operator >=(Trort t1, Trort t2)
        {
            return t1 == t2 || t1 > t2;
        }

        public static bool operator <=(Trort t1, Trort t2)
        {
            return t1 == t2 || t1 < t2;
        }
        #endregion
    }

    public static class TrortEx
    {
        public static Trort ToTrort(this IEnumerable<Trit> trits)
        {
            return new Trort(trits);
        }
    }
}
