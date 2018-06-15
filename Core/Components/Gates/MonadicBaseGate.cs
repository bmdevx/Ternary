using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    public abstract class MonadicBaseGate : Basic1In1OutComponent
    {
        public MonadicBaseGate(IComponentOutput component, Trit inputState = Trit.Neu) :
            base(component, inputState) { }

        public MonadicBaseGate(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu) :
            base(input, inputState) { }
    }
}
