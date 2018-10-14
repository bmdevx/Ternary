using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ternary.Tools;

namespace Ternary.Components
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class Basic18In9OutComponent : IMultiOutComponent
    {
        public ComponentTriggeredEvent[] Outputs { get; }

        public ComponentTriggeredEvent[] AInputs { get; }
        public ComponentTriggeredEvent[] BInputs { get; }

        protected Trit[] OutputStates { get; }

        protected Trit[] AInputStates { get; }
        protected Trit[] BInputStates { get; }

        public Tryte BusValue => new Tryte(OutputStates);

        protected string PinOutOfRange => $"Pin must be in range of 0 to {Tryte.NUMBER_OF_TRITS - 1}";
        internal virtual string DebuggerInfo => $"{BusValue.DebuggerInfo} - {ToString()}";
        public string ComponentName { get; internal set; }

        public Basic18In9OutComponent(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null)
        {
            Outputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            AInputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            BInputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];

            AInputStates = new Trit[Tryte.NUMBER_OF_TRITS];
            BInputStates = new Trit[Tryte.NUMBER_OF_TRITS];
            OutputStates = new Trit[Tryte.NUMBER_OF_TRITS];

            if (aInputStates != null)
            {
                int i = 0;
                foreach (Trit t in aInputStates)
                {
                    AInputStates[i] = t;
                    if (i == Tryte.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            if (bInputStates != null)
            {
                int i = 0;
                foreach (Trit t in bInputStates)
                {
                    BInputStates[i] = t;
                    if (i == Tryte.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                AInputs[i] += (s, t) => { AInputStates[i] = t; OnAInputInvoked(s, t, i); };
                BInputs[i] += (s, t) => { BInputStates[i] = t; OnBInputInvoked(s, t, i); };
            }
        }


        public Trit AInputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return AInputStates[pin];
        }

        public Trit BInputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return BInputStates[pin];
        }

        public Trit OutputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return OutputStates[pin];
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


        protected virtual void OnAInputInvoked(object sender, Trit trit, int pin) { }

        protected virtual void OnBInputInvoked(object sender, Trit trit, int pin) { }


        protected void InvokeOutput(int pin, Trit trit, object sender = null)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            OutputStates[pin] = trit;

            Outputs[pin]?.Invoke(sender ?? this, trit);
        }


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
            return String.Join(" | ", Create.NewTryteSizedArray(i => $"{i} {AInputStates[i].ToSymbol()}:{BInputStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}"));
        }
    }
}
