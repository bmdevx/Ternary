using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public abstract class Basic2In1OutComponent : IComponent
    {
        public event ComponentTriggeredEvent Output;

        public Trit Input1State { get; protected set; }
        public Trit Input2State { get; protected set; }


        public Basic2In1OutComponent(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu)
        {
            if (component1 != null)
                component1.Output += Input1;

            if (component2 != null)
                component2.Output += Input2;

            Input1State = input1State;
            Input2State = input2State;
        }

        public Basic2In1OutComponent(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu)
        {
            input1 += Input1;
            input2 += Input2;
            Input1State = input1State;
            Input2State = input2State;
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
            Input1State = trit;

            InvokeOutput(this, Execute(Input1State, Input2State));
        }

        protected virtual void OnInput2Invoked(object sender, Trit trit)
        {
            Input2State = trit;

            InvokeOutput(this, Execute(Input1State, Input2State));
        }


        public virtual void Input(Trit input1State, Trit input2State, object sender = null)
        {
            Input1State = input1State;
            Input2State = input2State;

            InvokeOutput(sender ?? this, Execute(input1State, input2State));
        }

        protected void InvokeOutput(object sender, Trit trit)
        {
            Output?.Invoke(sender, trit);
        }

        protected abstract Trit Execute(Trit input1State, Trit input2State);
    }
}
