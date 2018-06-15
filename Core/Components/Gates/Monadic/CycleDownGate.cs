using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates.Monadic
{
    public class CycleDownGate : MonadicBaseGate
    {
        public CycleDownGate(IComponentOutput component, Trit inputState = Trit.Neu) :
            base(component, inputState) { }

        public CycleDownGate(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu) :
            base(input, inputState) { }

        protected override Trit Execute(object sender, Trit trit)
        {
            return TritLogic.CycleDown(trit);
        }
    }
}
