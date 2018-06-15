using System;

namespace Ternary.Components
{
    public abstract class MultiIn1OutComponent : IComponentOutput, IMultiInComponent
    {
        public event ComponentTriggeredEvent Output;
        public ComponentTriggeredEvent[] Inputs { get; }

        public int NUMBER_OF_INPUTS { get; }
        
        protected Trit[] InputStates { get; }

        protected string PinOutOfRange => $"Input must be in range of 0 to {NUMBER_OF_INPUTS - 1}";


        public MultiIn1OutComponent(int numberOfInputs)
        {
            if (numberOfInputs < 1)
                throw new Exception("Invalid number of Inputs. Must be greater than 0");

            NUMBER_OF_INPUTS = numberOfInputs;

            Inputs = new ComponentTriggeredEvent[NUMBER_OF_INPUTS];
            InputStates = new Trit[NUMBER_OF_INPUTS];

            for (int inputPin = 0; inputPin < NUMBER_OF_INPUTS; inputPin++)
            {
                Inputs[inputPin] += (s, t) =>
                {
                    OnInputInvoked(inputPin, t, s);
                };
            }
        }

        
        public Trit InputState(int inputPin)
        {
            if (inputPin >= NUMBER_OF_INPUTS || inputPin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return InputStates[inputPin];
        }


        public void Input(int inputPin, Trit trit, object sender = null)
        {
            if (inputPin >= NUMBER_OF_INPUTS || inputPin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            Inputs[inputPin]?.Invoke(sender ?? this, trit);
        }

        public virtual void OnInputInvoked(int inputPin, Trit trit, object sender = null)
        {
            if (inputPin >= NUMBER_OF_INPUTS || inputPin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            InputStates[inputPin] = trit;

            InvokeOutput(sender, Execute());
        }


        protected void InvokeOutput(object sender, Trit trit)
        {
            Output?.Invoke(sender, trit);
        }


        protected abstract Trit Execute();


        public ComponentTriggeredEvent this[int inputPin]
        {
            get
            {
                return (NUMBER_OF_INPUTS > inputPin && inputPin > -1) ? Inputs[inputPin] :
                    throw new IndexOutOfRangeException(PinOutOfRange);
            }
        }
    }
}
