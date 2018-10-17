using System.Diagnostics;
using System.Linq;
using Ternary.Tools;

namespace Ternary.Components.Adders
{
    [DebuggerDisplay("{BusValue}")]
    public class TrortAdder : IBusComponentOutput<Trort>
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent CarryOut;
        public event ComponentBusTriggeredEvent<Trort> BusOutput;

        private FullAdder[] _Adders = Create.NewTrortSizedArray(i => new FullAdder());

        private Trit[] _Trits = new Trit[Trort.NUMBER_OF_TRITS];


        public Trort BusValue => new Trort(_Trits);

        public Trit CarryOutState { get; protected set; }


        private bool locker = true; 


        public TrortAdder()
        {
            _Adders[0].CarryOutput += _Adders[1].InputCarry;
            _Adders[0].SumOutput += (s, t) => { _Trits[0] = t; };

            _Adders[1].CarryOutput += _Adders[2].InputCarry;
            _Adders[1].SumOutput += (s, t) => { _Trits[1] = t; };

            _Adders[2].CarryOutput += _Adders[3].InputCarry;
            _Adders[2].SumOutput += (s, t) => { _Trits[2] = t; };

            _Adders[3].CarryOutput += _Adders[4].InputCarry;
            _Adders[3].SumOutput += (s, t) => { _Trits[3] = t; };

            _Adders[4].CarryOutput += _Adders[5].InputCarry;
            _Adders[4].SumOutput += (s, t) => { _Trits[4] = t; };

            _Adders[5].CarryOutput += _Adders[6].InputCarry;
            _Adders[5].SumOutput += (s, t) => { _Trits[5] = t; };

            _Adders[6].CarryOutput += _Adders[7].InputCarry;
            _Adders[6].SumOutput += (s, t) => { _Trits[6] = t; };

            _Adders[7].CarryOutput += _Adders[8].InputCarry;
            _Adders[7].SumOutput += (s, t) => { _Trits[7] = t; };

            _Adders[8].CarryOutput += _Adders[9].InputCarry;
            _Adders[8].SumOutput += (s, t) => { _Trits[8] = t; };

            _Adders[9].CarryOutput += _Adders[10].InputCarry;
            _Adders[9].SumOutput += (s, t) => { _Trits[9] = t; };

            _Adders[10].CarryOutput += _Adders[11].InputCarry;
            _Adders[10].SumOutput += (s, t) => { _Trits[10] = t; };

            _Adders[Trort.NUMBER_OF_TRITS - 1].CarryOutput += (s, carry) =>
            {
                CarryOutState = carry;

                if (!locker)
                {
                    CarryOut?.Invoke(this, carry);
                }
            };

            _Adders[Trort.NUMBER_OF_TRITS - 1].SumOutput += (s, sum) =>
            {
                _Trits[Trort.NUMBER_OF_TRITS - 1] = sum;

                if (!locker)
                {
                    BusOutput?.Invoke(this, BusValue);
                }
            };
        }


        public void ABusInput(object sender, Trort tryte)
        {
            for (int i = 0; i < Trort.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].AInput(this, tryte[i]);
            }
        }

        public void BBusInput(object sender, Trort tryte)
        {
            for (int i = 0; i < Trort.NUMBER_OF_TRITS - 1; i++)
            {
                _Adders[i].BInput(this, tryte[i]);
            }

            locker = false;
            _Adders[Trort.NUMBER_OF_TRITS - 1].BInput(this, tryte[Trort.NUMBER_OF_TRITS - 1]);
            locker = true;
        }

        public void CarryInput(object sender, Trit trit)
        {
            _Adders[0].InputCarry(sender, trit);
        }
    }
}
