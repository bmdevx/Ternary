using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TryteRegister : IBusComponent
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent BusOutput;


        public Trit ReadWriteState { get; private set; }

        public Tryte Value { get; private set; }

        public Tryte IncomingValue { get; private set; }


        private TernaryLatchGate[] _TernaryLatchGates = new TernaryLatchGate[Tryte.NUMBER_OF_TRITS];


        public TryteRegister()
        {

        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            if (state == Trit.Neg)
                BusOutput?.Invoke(this, Value);
            else
                Value = IncomingValue;
        }


        public void BusInput(object sender, Tryte tryte)
        {
            IncomingValue = tryte;

            if (ReadWriteState == Trit.Pos)
                Value = IncomingValue;
        }
    }
}
