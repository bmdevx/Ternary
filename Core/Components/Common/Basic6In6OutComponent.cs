using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components
{
    public abstract class Basic6In6OutComponent : IMultiIOComponent
    {
        public ComponentTriggeredEvent[] Outputs { get; }
        public ComponentTriggeredEvent[] Inputs { get; }


        protected Trit[] OutputStates { get; }
        protected Trit[] InputStates { get; }

        public Tryte BusValue => new Tryte(OutputStates);

        protected string PinOutOfRange => $"Pin must be in range of 0 to {Tryte.NUMBER_OF_TRITS - 1}";
        internal virtual string DebuggerInfo => $"{BusValue.DebuggerInfo} - {ToString()}";


        public Basic6In6OutComponent(IEnumerable<Trit> inputStates = null)
        {
            Outputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            Inputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];

            InputStates = new Trit[Tryte.NUMBER_OF_TRITS];
            OutputStates = new Trit[Tryte.NUMBER_OF_TRITS];

            if (inputStates != null)
            {
                int i = 0;
                foreach (Trit t in inputStates)
                {
                    InputStates[i] = t;
                    if (i == Tryte.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                Inputs[i] += (s, t) => InputInvoked(s, t, i);
            }
        }


        public Trit InputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return InputStates[pin];
        }

        public Trit OutputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return OutputStates[pin];
        }


        protected virtual void InputInvoked(object sender, Trit trit, int pin)
        {
            InputStates[pin] = trit;
            Output(pin, Execute(sender, InputStates[pin]), this);
        }


        public void Input(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            Inputs[pin]?.Invoke(sender ?? this, trit);
        }

        protected void Output(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            OutputStates[pin] = trit;

            Outputs[pin]?.Invoke(sender ?? this, trit);
        }


        protected abstract Trit Execute(object sender, Trit inputState);


        public ComponentTriggeredEvent this[int pin]
        {
            get
            {
                return (Tryte.NUMBER_OF_TRITS > pin && pin > -1) ? Inputs[pin] :
                    throw new IndexOutOfRangeException(PinOutOfRange);
            }
            set
            {
                if (Tryte.NUMBER_OF_TRITS > pin && pin > -1) Outputs[pin] += value;
                else throw new IndexOutOfRangeException(PinOutOfRange);
            }
        }


        public override string ToString()
        {
            return String.Join(" | ", Enumerable.Range(0, Tryte.NUMBER_OF_TRITS)
                .Select(i => $"{i} {InputStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}"));
        }
    }
}
