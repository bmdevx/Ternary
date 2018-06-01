using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary
{
    public class Tryte : IEnumerable<Trit>, IComparable, IComparable<Tryte>, IEquatable<Tryte>, IFormattable//, IConvertible
    {
        public const int NUMBER_OF_TRITS = 6;

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
            for (int i = 0; i < NUMBER_OF_TRITS; i++)
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
            //TODO as number
            return base.ToString();
        }

        public Trit this[int index]
        {
            get { return index > -1 && index < 6 ? _Trits[index] : throw new IndexOutOfRangeException(); }

            set { if (index > -1 && index < 6) _Trits[index] = value; else throw new IndexOutOfRangeException(); }
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


        public Tryte Invert()
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
                if (TritTools.TryParse(value[i], out Trit trit))
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
                if (TritTools.TryParse(value[i], out Trit trit))
                    tryte[i] = trit;
                else
                    return false;
            }

            return true;
        }


        #region Overloads
        public static implicit operator Tryte(byte value)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Tryte(short value)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Tryte(int value)
        {
            throw new NotImplementedException();
        }


        public static Tryte operator +(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator -(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator *(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator /(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator %(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }


        public static Tryte operator ++(Tryte tryte)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator --(Tryte tryte)
        {
            throw new NotImplementedException();
        }

        public static Tryte operator -(Tryte tryte)
        {
            //inverts value not trits
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static bool operator <(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Tryte t1, Tryte t2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
