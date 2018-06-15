using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates.Monadic
{
    public class ReverseDiode : MonadicBaseGate
    {
        public ReverseDiode(IComponentOutput component, Trit inputState = Trit.Neu) :
            base(component, inputState) { }

        public ReverseDiode(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu) :
            base(input, inputState) { }

        protected override Trit Execute(object sender, Trit trit)
        {
            return TritLogic.Reverse(trit);
        }
    }
}
