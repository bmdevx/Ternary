namespace Ternary.Components.Gates
{
    public abstract class DyadicBaseGate : Basic2In1OutComponent
    {
        public DyadicBaseGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public DyadicBaseGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override void OnAInputInvoked(object sender, Trit trit)
        {
            InvokeOutput(this, Execute(AInputState, BInputState));
        }

        protected override void OnBInputInvoked(object sender, Trit trit)
        {
            InvokeOutput(this, Execute(AInputState, BInputState));
        }

        protected override void OnInputInvoked(object sender, Trit inputStateA, Trit inputStateB)
        {
            InvokeOutput(sender ?? this, Execute(inputStateA, inputStateB));
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);
    }
}
