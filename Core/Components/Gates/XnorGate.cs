using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //XNOR
    public class XnorGate : BasicGate
    {
        public XnorGate(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(component1, component2, input1State, input2State) { }

        public XnorGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(input1, input2, input1State, input2State) { }


        protected override Trit Execute(Trit input1State, Trit input2State)
        {
            switch (input1State)
            {
                case Trit.Neg: return input2State.Invert();
                case Trit.Neu: return Trit.Neu;
                case Trit.Pos: return input2State;
            }

            return Trit.Neu;
        }
    }
}
