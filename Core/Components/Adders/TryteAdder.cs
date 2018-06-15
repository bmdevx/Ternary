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
        public event ComponentBusTriggeredEvent Output;

        private FullAdder[] _Adders = Enumerable.Range(0, Tryte.NUMBER_OF_TRITS).Select(i => new FullAdder()).ToArray();

        public Tryte BusValue => _Adders.Select(a => a.SumValue).ToTryte();


        public TryteAdder()
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS - 1; i++)
            {
                _Adders[i].CarryOutput += _Adders[i + 1].InputCarry;
            }

            _Adders[Tryte.NUMBER_OF_TRITS - 1].CarryOutput += (s, carry) =>
            {
                Output?.Invoke(this, BusValue);
                CarryOut?.Invoke(this, carry);
            };
        }


        public void InputA(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].InputA(sender, tryte[i]);
            }
        }

        public void InputB(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _Adders[i].InputB(sender, tryte[i]);
            }
        }

        public void InputCarry(object sender, Trit trit)
        {
            _Adders[0].InputCarry(sender, trit);
        }
    }
}
