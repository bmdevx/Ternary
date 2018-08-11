using System;
using System.Diagnostics;
using System.Linq;

namespace Ternary.Components.Chips
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class IChip : IMultiIOComponent
    {
        public abstract int NUMBER_OF_PINS { get; }

        public ComponentTriggeredEvent[] Outputs { get; }
        public ComponentTriggeredEvent[] Inputs { get; }

        protected Trit[] PinStates { get; }

        protected string PinOutOfRange => $"Pin must be in range of 0 to {NUMBER_OF_PINS - 1}";
        internal string DebuggerInfo => ToString();
        public string ComponentName { get; internal set; }


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


        public override string ToString()
        {
            return String.Join(" | ", Enumerable.Range(0, NUMBER_OF_PINS)
                .Select(i => $"{i}: {PinStates[i].ToSymbol()}"));
        }
    }
}
