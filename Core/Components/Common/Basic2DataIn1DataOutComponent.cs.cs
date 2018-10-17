using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ternary.Tools;

namespace Ternary.Components
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class Basic2DataIn1DataOutComponent<T> : IMultiOutComponent where T : ITernaryDataType, new()
    {
        protected T DataType = new T();

        public ComponentTriggeredEvent[] Outputs { get; }

        public ComponentTriggeredEvent[] AInputs { get; }
        public ComponentTriggeredEvent[] BInputs { get; }

        protected Trit[] OutputStates { get; }

        protected Trit[] AInputStates { get; }
        protected Trit[] BInputStates { get; }

        public T BusValue => (T)DataType.CreateFromTrits(OutputStates);

        protected string PinOutOfRange => $"Pin must be in range of 0 to {DataType.NUMBER_OF_TRITS - 1}";
        internal virtual string DebuggerInfo => $"{DataType.DebuggerInfo} - {ToString()}";
        public string ComponentName { get; internal set; }

        public Basic2DataIn1DataOutComponent(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null)
        {
            Outputs = new ComponentTriggeredEvent[DataType.NUMBER_OF_TRITS];
            AInputs = new ComponentTriggeredEvent[DataType.NUMBER_OF_TRITS];
            BInputs = new ComponentTriggeredEvent[DataType.NUMBER_OF_TRITS];

            AInputStates = new Trit[DataType.NUMBER_OF_TRITS];
            BInputStates = new Trit[DataType.NUMBER_OF_TRITS];
            OutputStates = new Trit[DataType.NUMBER_OF_TRITS];

            if (aInputStates != null)
            {
                int i = 0;
                foreach (Trit t in aInputStates)
                {
                    AInputStates[i] = t;
                    if (i == DataType.NUMBER_OF_TRITS - 1)
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
                    if (i == DataType.NUMBER_OF_TRITS - 1)
                        break;

                    i++;
                }
            }

            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                AInputs[i] += (s, t) => { AInputStates[i] = t; OnAInputInvoked(s, t, i); };
                BInputs[i] += (s, t) => { BInputStates[i] = t; OnBInputInvoked(s, t, i); };
            }
        }


        public Trit AInputState(int pin)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return AInputStates[pin];
        }

        public Trit BInputState(int pin)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return BInputStates[pin];
        }

        public Trit OutputState(int pin)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return OutputStates[pin];
        }


        public void AInput(int pin, Trit trit, object sender = null)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            AInputs[pin]?.Invoke(sender ?? this, trit);
        }

        public void BInput(int pin, Trit trit, object sender = null)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            BInputs[pin]?.Invoke(sender ?? this, trit);
        }


        protected virtual void OnAInputInvoked(object sender, Trit trit, int pin) { }

        protected virtual void OnBInputInvoked(object sender, Trit trit, int pin) { }


        protected void InvokeOutput(int pin, Trit trit, object sender = null)
        {
            if (pin >= DataType.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            OutputStates[pin] = trit;

            Outputs[pin]?.Invoke(sender ?? this, trit);
        }


        public ComponentTriggeredEvent this[int pin]
        {
            set
            {
                if (DataType.NUMBER_OF_TRITS > pin && pin > -1) Outputs[pin] += value;
                else throw new IndexOutOfRangeException(PinOutOfRange);
            }
        }


        public override string ToString()
        {
            return String.Join(" | ", Create.NewDataSizedSizedArray<string, T>(i => $"{i} {AInputStates[i].ToSymbol()}:{BInputStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}"));
        }
    }
}
