using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ternary
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class Tryte : IEnumerable<Trit>, IComparable, IComparable<Tryte>, IEquatable<Tryte>, IFormattable//, IConvertible
    {
        public const int NUMBER_OF_TRITS = 6;

        public const int MAX_INT_VALUE = 364;
        public const int MIN_INT_VALUE = -364;

        public static readonly Tryte MAX_VALUE = new Tryte(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);
        public static readonly Tryte MIN_VALUE = new Tryte(Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg, Trit.Neg);
        
        internal string DebuggerInfo => $"{ToString()} ({ToString("s")})";


        private readonly Trit[] _Trits = new Trit[NUMBER_OF_TRITS];

        public Trit[] Trits => _Trits.Clone() as Trit[];
        
        public Trit T0 { get { return _Trits[0]; } set { _Trits[0] = value; } }
        public Trit T1 { get { return _Trits[1]; } set { _Trits[1] = value; } }
        public Trit T2 { get { return _Trits[2]; } set { _Trits[2] = value; } }
        public Trit T3 { get { return _Trits[3]; } set { _Trits[3] = value; } }
        public Trit T4 { get { return _Trits[4]; } set { _Trits[4] = value; } }
        public Trit T5 { get { return _Trits[5]; } set { _Trits[5] = value; } }


        public Tryte(Trit t0 = Trit.Neu, Trit t1 = Trit.Neu, Trit t2 = Trit.Neu,
            Trit t3 = Trit.Neu, Trit t4 = Trit.Neu, Trit t5 = Trit.Neu)
        {
            _Trits[0] = t0;
            _Trits[1] = t1;
            _Trits[2] = t2;
            _Trits[3] = t3;
            _Trits[4] = t4;
            _Trits[5] = t5;
        }

        public Tryte(IEnumerable<Trit> trits)
        {
            int i = 0;
            foreach (Trit t in trits)
            {
                _Trits[i] = t;

                if (i == NUMBER_OF_TRITS - 1)
                    break;

                i++;
            }
        }

        public IEnumerator<Trit> GetEnumerator()
        {
            foreach (Trit trit in _Trits)
                yield return trit;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Trits.GetEnumerator();
        }


        public int CompareTo(object obj)
        {
            if (obj is Tryte tryte)
                return CompareTo(tryte);
            return -1;
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
            if (obj is Tryte tryte)
                return this.Equals(tryte);
            return false;
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
            get { return index > -1 && index < NUMBER_OF_TRITS ? _Trits[index] : throw new IndexOutOfRangeException(); }

            set { if (index > -1 && index < NUMBER_OF_TRITS) _Trits[index] = value; else throw new IndexOutOfRangeException(); }
        }


        public int ToInt()
        {
            int s = 0;

            int m = 1;
            foreach (Trit t in _Trits)
            {
                s += m * t.Value();
                m *= 3;
            }

            return s;
        }


        private Tryte Invert()
        {
            return new Tryte(
                T0.Invert(),
                T1.Invert(),
                T2.Invert(),
                T3.Invert(),
                T4.Invert(),
                T5.Invert());
        }


        public static Tryte Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            Trit[] trits = new Trit[Tryte.NUMBER_OF_TRITS];

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS && i < value.Length; i++)
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

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS && i < value.Length; i++)
            {
                if (TritEx.TryParse(value[i], out Trit trit))
                    tryte[i] = trit;
                else
                    return false;
            }

            return true;
        }


        #region Overloads

        public static implicit operator Tryte(int value)
        {
            if (value >= MAX_INT_VALUE)
                return MAX_VALUE;
            else if (value <= MIN_INT_VALUE)
                return MIN_VALUE;

            Tryte tryte = new Tryte();
            for (int i = 0; value != 0 && i < NUMBER_OF_TRITS; i++)
            {
                switch (value % 3)
                {
                    case -1:
                    case 2: tryte[i] = Trit.Neg; break;
                    case -2:
                    case 1: tryte[i] = Trit.Pos; break;
                    default: tryte[i] = Trit.Neu; break;
                }

                value = (value < 0 ? (value - 1) : (value + 1)) / 3;
            }

            return tryte;
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

        public static Tryte operator -(Tryte t1, Tryte t2)
        {
            return t1 + t2.Invert();
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
}
