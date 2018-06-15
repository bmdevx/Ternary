using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates.Monadic
{
    public class ShiftDownGate : MonadicBaseGate
    {
        public ShiftDownGate(IComponentOutput component, Trit inputState = Trit.Neu) :
            base(component, inputState) { }

        public ShiftDownGate(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu) :
            base(input, inputState) { }

        protected override Trit Execute(object sender, Trit trit)
        {
            return TritLogic.ShiftDown(trit);
        }
    }
}
