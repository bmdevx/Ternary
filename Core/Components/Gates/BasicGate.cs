using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    public abstract class BasicGate : IComponent
    {
        public event ComponentTriggeredEvent Output;

        public Trit Input1State { get; protected set; }
        public Trit Input2State { get; protected set; }
        
        
        public BasicGate(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu)
        {
            if (component1 != null)
                component1.Output += Input1;

            if (component2 != null)
                component2.Output += Input2;

            Input1State = input1State;
            Input2State = input2State;
        }

        public BasicGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu)
        {
            input1 += Input1;
            input2 += Input2;
            Input1State = input1State;
            Input2State = input2State;
        }


        public void Input1(object sender, Trit trit)
        {
            OnInput1Triggered(sender, trit);
        }

        public void Input2(object sender, Trit trit)
        {
            OnInput2Triggered(sender, trit);
        }


        protected virtual void OnInput1Triggered(object sender, Trit trit)
        {
            Input1State = trit;

            Output?.Invoke(this, Execute(Input1State, Input2State));
        }

        protected virtual void OnInput2Triggered(object sender, Trit trit)
        {
            Input2State = trit;

            Output?.Invoke(this, Execute(Input1State, Input2State));
        }


        public void Trigger(Trit input1State, Trit input2State)
        {
            Input1State = input1State;
            Input2State = input2State;

            Output?.Invoke(this, Execute(input1State, input2State));
        }

        protected abstract Trit Execute(Trit input1State, Trit input2State);
    }
}
