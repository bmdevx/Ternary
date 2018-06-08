using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    public abstract class GateBase : Basic2In1OutComponent
    {
        public GateBase(IComponentOutput componentA, IComponentOutput componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public GateBase(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }
    }
}
