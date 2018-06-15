namespace Ternary.Components.Gates.Dyadic
{
    //Converse Implication
    public class ConverseImplicationGate : DyadicBaseGate
    {
        public ConverseImplicationGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ConverseImplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.ConverseImplication(inputStateA, inputStateB);
        }
    }
}
