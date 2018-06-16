using System.Diagnostics;

namespace Ternary.Components
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public abstract class Basic1In1OutComponent : IComponent
    {
        public event ComponentTriggeredEvent Output;

        public Trit InputState { get; protected set; }
        public Trit OutputState { get; protected set; }

        internal string DebuggerInfo => ToString();


        public Basic1In1OutComponent(IComponentOutput component, Trit inputState = Trit.Neu)
        {
            if (component != null)
                component.Output += Input;

            InputState = inputState;
        }

        public Basic1In1OutComponent(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu)
        {
            input += Input;
            InputState = inputState;
        }


        public void Input(object sender, Trit trit)
        {
            InputState = trit;
            OnInputInvoked(sender, trit);
        }


        protected virtual void OnInputInvoked(object sender, Trit trit) { }


        protected void InvokeOutput(object sender, Trit trit)
        {
            OutputState = trit;
            Output?.Invoke(sender, trit);
        }


        public override string ToString()
        {
            return $"{InputState.ToSymbol()}>{OutputState.ToSymbol()}";
        }
    }
}
