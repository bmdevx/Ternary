using System.Diagnostics;

namespace Ternary.Components
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class Basic2In1OutComponent : IComponentOutput
    {
        public event ComponentTriggeredEvent Output;

        public Trit AInputState { get; protected set; }
        public Trit BInputState { get; protected set; }
        public Trit OutputState { get; protected set; }

        internal string DebuggerInfo => ToString();
        public string ComponentName { get; internal set; }


        public Basic2In1OutComponent(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            if (componentA != null)
                componentA.Output += AInput;

            if (componentB != null)
                componentB.Output += BInput;

            AInputState = inputStateA;
            BInputState = inputStateB;
        }

        public Basic2In1OutComponent(ComponentTriggeredEvent inputA = null, ComponentTriggeredEvent inputB = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu)
        {
            inputA += AInput;
            inputB += BInput;
            AInputState = inputStateA;
            BInputState = inputStateB;
        }


        public void AInput(object sender, Trit trit)
        {
            AInputState = trit;

            OnAInputInvoked(sender, trit);
        }

        public void BInput(object sender, Trit trit)
        {
            BInputState = trit;

            OnBInputInvoked(sender, trit);
        }


        protected virtual void OnAInputInvoked(object sender, Trit trit) { }

        protected virtual void OnBInputInvoked(object sender, Trit trit) { }


        public virtual void Input(Trit inputStateA, Trit inputStateB, object sender = null)
        {
            AInputState = inputStateA;
            BInputState = inputStateB;

            OnInputInvoked(sender, inputStateA, inputStateB);
        }

        protected virtual void OnInputInvoked(object sender, Trit inputStateA, Trit inputStateB) { }


        protected void InvokeOutput(object sender, Trit trit)
        {
            OutputState = trit;
            Output?.Invoke(sender, trit);
        }
        

        public override string ToString()
        {
            return $"{AInputState.ToSymbol()}:{BInputState.ToSymbol()}>{OutputState.ToSymbol()}";
        }
    }
}
