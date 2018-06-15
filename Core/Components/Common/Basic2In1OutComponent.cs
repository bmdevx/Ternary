using System.Diagnostics;

namespace Ternary.Components
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class Basic2In1OutComponent : IComponentOutput
    {
        public event ComponentTriggeredEvent Output;

        public Trit InputStateA { get; protected set; }
        public Trit InputStateB { get; protected set; }
        public Trit OutputState { get; protected set; }

        internal string DebuggerInfo => ToString();


        public Basic2In1OutComponent(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            if (componentA != null)
                componentA.Output += InputA;

            if (componentB != null)
                componentB.Output += InputB;

            InputStateA = inputStateA;
            InputStateB = inputStateB;
        }

        public Basic2In1OutComponent(ComponentTriggeredEvent inputA = null, ComponentTriggeredEvent inputB = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            inputA += InputA;
            inputB += InputB;
            InputStateA = inputStateA;
            InputStateB = inputStateB;
        }


        public void InputA(object sender, Trit trit)
        {
            OnInputAInvoked(sender, trit);
        }

        public void InputB(object sender, Trit trit)
        {
            OnInputBInvoked(sender, trit);
        }


        protected virtual void OnInputAInvoked(object sender, Trit trit)
        {
            InputStateA = trit;

            InvokeOutput(this, Execute(InputStateA, InputStateB));
        }

        protected virtual void OnInputBInvoked(object sender, Trit trit)
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
            OutputState = trit;
            Output?.Invoke(sender, trit);
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);


        public override string ToString()
        {
            return $"{InputStateA.ToSymbol()}:{InputStateB.ToSymbol()}>{OutputState.ToSymbol()}";
        }
    }
}
