using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class BusBase : IMultiOutComponent
    {
        public ComponentTriggeredEvent[] Outputs { get; }

        public ComponentTriggeredEvent[] AInputs { get; }
        public ComponentTriggeredEvent[] BInputs { get; }

        protected Trit[] OutputStates { get; }

        protected Trit[] APinStates { get; }
        protected Trit[] BPinStates { get; }

        public Tryte BusValue => new Tryte(OutputStates);
        
        protected string PinOutOfRange => $"Pin must be in range of 0 to {Tryte.NUMBER_OF_TRITS - 1}";
        internal string DebuggerInfo => $"{BusValue.DebuggerInfo} - {ToString()}";


        public BusBase(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null)
        {
            Outputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            AInputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            BInputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];

            APinStates = new Trit[Tryte.NUMBER_OF_TRITS];
            BPinStates = new Trit[Tryte.NUMBER_OF_TRITS];
            OutputStates = new Trit[Tryte.NUMBER_OF_TRITS];

            if (aPinStates != null)
            {
                int i = 0;
                foreach (Trit t in aPinStates)
                {
                    APinStates[i] = t;
                    if (i == Tryte.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            if (bPinStates != null)
            {
                int i = 0;
                foreach (Trit t in bPinStates)
                {
                    BPinStates[i] = t;
                    if (i == Tryte.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                AInputs[i] += (s, t) => APinInvoked(s, t, i);
                BInputs[i] += (s, t) => APinInvoked(s, t, i);
            }
        }


        public Trit APinState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return APinStates[pin];
        }

        public Trit BPinState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return BPinStates[pin];
        }


        private void APinInvoked(object sender, Trit trit, int pin)
        {
            APinStates[pin] = trit;
            Output(pin, Execute(APinStates[pin], BPinStates[pin]), this);
        }

        private void BPinInvoked(object sender, Trit trit, int pin)
        {
            BPinStates[pin] = trit;
            Output(pin, Execute(APinStates[pin], BPinStates[pin]), this);
        }


        public void AInput(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            AInputs[pin]?.Invoke(sender ?? this, trit);
        }

        public void BInput(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            BInputs[pin]?.Invoke(sender ?? this, trit);
        }

        protected void Output(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            OutputStates[pin] = trit;

            Outputs[pin]?.Invoke(sender ?? this, trit);
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);


        public ComponentTriggeredEvent this[int pin]
        {
            set
            {
                if (Tryte.NUMBER_OF_TRITS > pin && pin > -1) Outputs[pin] += value;
                else throw new IndexOutOfRangeException(PinOutOfRange);
            }
        }


        public override string ToString()
        {
            return String.Join(" | ", Enumerable.Range(0, Tryte.NUMBER_OF_TRITS)
                .Select(i => $"{i} {APinStates[i].ToSymbol()}:{BPinStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}"));
        }
    }
}
