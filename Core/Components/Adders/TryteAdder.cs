using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Buses.Dyadic;

namespace Ternary.Components.Adders
{
    public class TryteAdder : IBusComponentOutput
    {
        public event ComponentTriggeredEvent CarryOut;
        public event ComponentBusTriggeredEvent BusOutput;

        private FullAdder[] _Adders = Enumerable.Range(0, Tryte.NUMBER_OF_TRITS).Select(i => new FullAdder()).ToArray();

        private Trit[] _Trits = new Trit[Tryte.NUMBER_OF_TRITS];

        public Tryte BusValue => new Tryte(_Trits);


        public TryteAdder()
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS - 1; i++)
            {
                _Adders[i].CarryOutput += _Adders[i + 1].InputCarry;
                _Adders[i].SumOutput += (s, t) => _Trits[i] = t;
            }

            _Adders[Tryte.NUMBER_OF_TRITS - 1].CarryOutput += (s, carry) =>
            {
                CarryOut?.Invoke(this, carry);
                BusOutput?.Invoke(this, BusValue);
            };
        }


        public void ABusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].AInput(sender, tryte[i]);
            }
        }

        public void BBusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].BInput(sender, tryte[i]);
            }
        }

        public void CarryInput(object sender, Trit trit)
        {
            _Adders[0].InputCarry(sender, trit);
        }
    }
}
