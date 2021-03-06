﻿using System.Diagnostics;
using System.Linq;
using Ternary.Tools;

namespace Ternary.Components.Adders
{
    [DebuggerDisplay("{BusValue}")]
    public class TryteAdder : IBusComponentOutput<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent CarryOut;
        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        private FullAdder[] _Adders = Create.NewTryteSizedArray(i => new FullAdder());

        private Trit[] _Trits = new Trit[Tryte.NUMBER_OF_TRITS];


        public Tryte BusValue => new Tryte(_Trits);

        public Trit CarryOutState { get; protected set; }


        private bool locker = true; 


        public TryteAdder()
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

            _Adders[Tryte.NUMBER_OF_TRITS - 1].CarryOutput += (s, carry) =>
            {
                CarryOutState = carry;

                if (!locker)
                {
                    CarryOut?.Invoke(this, carry);
                }
            };

            _Adders[Tryte.NUMBER_OF_TRITS - 1].SumOutput += (s, sum) =>
            {
                _Trits[Tryte.NUMBER_OF_TRITS - 1] = sum;

                if (!locker)
                {
                    BusOutput?.Invoke(this, BusValue);
                }
            };
        }


        public void ABusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].AInput(this, tryte[i]);
            }
        }

        public void BBusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS - 1; i++)
            {
                _Adders[i].BInput(this, tryte[i]);
            }

            locker = false;
            _Adders[Tryte.NUMBER_OF_TRITS - 1].BInput(this, tryte.T5);
            locker = true;
        }

        public void CarryInput(object sender, Trit trit)
        {
            _Adders[0].InputCarry(sender, trit);
        }
    }
}
