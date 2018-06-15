namespace Ternary.Components.Gates.Dyadic
{
    //AntiMin (NAND)
    public class AntiMinGate : DyadicBaseGate
    {
        public AntiMinGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public AntiMinGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.AntiMin(inputStateA, inputStateB);
        }
    }
}
