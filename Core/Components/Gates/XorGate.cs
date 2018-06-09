namespace Ternary.Components.Gates
{
    //XOR
    public class XorGate : GateBase
    {
        public XorGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public XorGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Xnor(inputStateA, inputStateB);
        }
    }
}
