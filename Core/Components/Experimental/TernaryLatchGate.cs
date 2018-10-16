using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TernaryLatchGate : IComponent
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;


        public Trit ReadWriteState { get; private set; }

        public Trit Value { get; private set; }

        public Trit IncomingValue { get; private set; }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            ReadWriteState = state;

            if (state == Trit.Neg)
                Output?.Invoke(this, Value);
            else if (state == Trit.Pos)
                Value = IncomingValue;
        }
        /// <summary>
        /// Sets the Value for the Gate
        /// </summary>
        /// <param name="trit">Value for the Gate</param>
        public void Input(object sender, Trit trit)
        {
            IncomingValue = trit;

            if (ReadWriteState == Trit.Pos)
                Value = IncomingValue;
        }
    }
}
