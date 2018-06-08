using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Nonimplication
    public class NonimplicationGate : GateBase
    {
        public NonimplicationGate(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public NonimplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Nonimplication(inputStateA, inputStateB);
        }
    }
}
