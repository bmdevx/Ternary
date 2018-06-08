using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public abstract class Basic2In1OutComponent : IComponent
    {
        public event ComponentTriggeredEvent Output;

        public Trit InputStateA { get; protected set; }
        public Trit InputStateB { get; protected set; }


        public Basic2In1OutComponent(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            if (componentA != null)
                componentA.Output += Input1;

            if (componentB != null)
                componentB.Output += Input2;

            InputStateA = inputStateA;
            InputStateB = inputStateB;
        }

        public Basic2In1OutComponent(ComponentTriggeredEvent inputA = null, ComponentTriggeredEvent inputB = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            inputA += Input1;
            inputB += Input2;
            InputStateA = inputStateA;
            InputStateB = inputStateB;
        }


        public void Input1(object sender, Trit trit)
        {
            OnInput1Invoked(sender, trit);
        }

        public void Input2(object sender, Trit trit)
        {
            OnInput2Invoked(sender, trit);
        }


        protected virtual void OnInput1Invoked(object sender, Trit trit)
        {
            InputStateA = trit;

            InvokeOutput(this, Execute(InputStateA, InputStateB));
        }

        protected virtual void OnInput2Invoked(object sender, Trit trit)
        {
            InputStateB = trit;

            InvokeOutput(this, Execute(InputStateA, InputStateB));
        }


        public virtual void Input(Trit inputStateA, Trit inputStateB, object sender = null)
        {
            InputStateA = inputStateA;
            InputStateB = inputStateB;

            InvokeOutput(sender ?? this, Execute(inputStateA, inputStateB));
        }

        protected void InvokeOutput(object sender, Trit trit)
        {
            Output?.Invoke(sender, trit);
        }

        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);
    }
}
