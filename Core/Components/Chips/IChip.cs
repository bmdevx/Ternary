using System;
using System.Collections.Generic;
using System.Text;
using Ternary;
using Ternary.Components;

namespace Ternary.Components.Chips
{
    public abstract class IChip : IMultiIOComponent
    {
        public abstract int NUMBER_OF_PINS { get; }

        public ComponentTriggeredEvent[] Outputs { get; }
        public ComponentTriggeredEvent[] Inputs { get; }

        protected Trit[] PinStates { get; }

        protected string PinOutOfRange => $"Pin must be in range of 0 to {NUMBER_OF_PINS - 1}";


        public IChip()
        {
            Outputs = new ComponentTriggeredEvent[NUMBER_OF_PINS];
            Inputs = new ComponentTriggeredEvent[NUMBER_OF_PINS];

            PinStates = new Trit[NUMBER_OF_PINS];
        }


        public Trit PinState(int pin)
        {
            if (pin >= NUMBER_OF_PINS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return PinStates[pin];
        }


        public void Input(int pin, Trit trit, object sender = null)
        {
            if (pin >= NUMBER_OF_PINS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            Inputs[pin]?.Invoke(sender ?? this, trit);
        }

        protected void Output(int pin, Trit trit, object sender = null)
        {
            if (pin >= NUMBER_OF_PINS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            Outputs[pin]?.Invoke(sender ?? this, trit);
        }


        public ComponentTriggeredEvent this[int pin]
        {
            get {
                return (NUMBER_OF_PINS > pin && pin > -1) ? Inputs[pin] :
                    throw new IndexOutOfRangeException(PinOutOfRange);
            }
            set {
                if (NUMBER_OF_PINS > pin && pin > -1) Outputs[pin] += value;
                    else throw new IndexOutOfRangeException(PinOutOfRange);
            }
        }
    }
}
