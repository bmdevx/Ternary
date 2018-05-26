using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ternary
{
    public class Tryte : IEnumerable<Trit>
    {
        private readonly Trit[] _Trits = new Trit[6];

        public Trit[] Trits => _Trits.Clone() as Trit[];
        
        public Trit T0 { get { return _Trits[0]; } set { _Trits[0] = value; } }
        public Trit T1 { get { return _Trits[1]; } set { _Trits[1] = value; } }
        public Trit T2 { get { return _Trits[2]; } set { _Trits[2] = value; } }
        public Trit T3 { get { return _Trits[3]; } set { _Trits[3] = value; } }
        public Trit T4 { get { return _Trits[4]; } set { _Trits[4] = value; } }
        public Trit T5 { get { return _Trits[5]; } set { _Trits[5] = value; } }


        public Tryte() : this(Trit.Neu, Trit.Neu, Trit.Neu, Trit.Neu, Trit.Neu, Trit.Neu) { }

        public Tryte(Trit t0, Trit t1, Trit t2, Trit t3, Trit t4, Trit t5)
        {
            _Trits[0] = t0;
            _Trits[1] = t1;
            _Trits[2] = t2;
            _Trits[3] = t3;
            _Trits[4] = t4;
            _Trits[5] = t5;
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

        public Trit this[int index]
        {
            get { return index > -1 && index < 6 ? _Trits[index] : throw new IndexOutOfRangeException(); }

            set { if (index > -1 && index < 6) _Trits[index] = value; else throw new IndexOutOfRangeException(); }
        }
    }
}
