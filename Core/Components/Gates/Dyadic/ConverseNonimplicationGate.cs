namespace Ternary.Components.Gates.Dyadic
{
    //Converse Nonimplication
    public class ConverseNonimplicationGate : DyadicBaseGate
    {
        public ConverseNonimplicationGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ConverseNonimplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.ConverseNonimplication(inputStateA, inputStateB);
        }
    }
}
