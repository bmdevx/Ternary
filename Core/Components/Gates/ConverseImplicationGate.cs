using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Converse Implication
    public class ConverseImplicationGate : BasicGate
    {
        public ConverseImplicationGate(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(component1, component2, input1State, input2State) { }

        public ConverseImplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(input1, input2, input1State, input2State) { }


        protected override Trit Execute(Trit input1State, Trit input2State)
        {
            switch (input1State)
            {
                case Trit.Neg: return input2State.Invert();
                case Trit.Neu: return input2State == Trit.Neg ? Trit.Pos : Trit.Neu;
                case Trit.Pos: return Trit.Pos;
            }

            return Trit.Neu;
        }
    }
}
