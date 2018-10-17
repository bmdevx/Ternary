using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Tools;

namespace Ternary.Components.Experimental
{
    public class TryteRegister : IBusComponent<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;


        public Trit ReadWriteState { get; private set; }
        
        private Trit[] _Trits = new Trit[Tryte.NUMBER_OF_TRITS];

        public Tryte Value => new Tryte(_Trits);


        private TernaryLatchGate[] _TernaryLatchGates = Create.NewTryteSizedArray(i => new TernaryLatchGate());


        public TryteRegister()
        {
            _TernaryLatchGates[0].Output += (s, t) => { _Trits[0] = t; };
            _TernaryLatchGates[1].Output += (s, t) => { _Trits[1] = t; };
            _TernaryLatchGates[2].Output += (s, t) => { _Trits[2] = t; };
            _TernaryLatchGates[3].Output += (s, t) => { _Trits[3] = t; };
            _TernaryLatchGates[4].Output += (s, t) => { _Trits[4] = t; };
            _TernaryLatchGates[Tryte.NUMBER_OF_TRITS - 1].Output += (s, t) =>
            {
                _Trits[Tryte.NUMBER_OF_TRITS - 1] = t;
                BusOutput?.Invoke(this, Value);
            };
        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            ReadWriteState = state;

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _TernaryLatchGates[i].ReadWriteEnabled(this, state);
            }
        }


        public void BusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                _TernaryLatchGates[i].Input(this, tryte[i]);
            }
        }
    }
}
