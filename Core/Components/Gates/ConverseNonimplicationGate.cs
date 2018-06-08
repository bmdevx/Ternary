using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Converse Nonimplication
    public class ConverseNonimplicationGate : BasicGate
    {
        public ConverseNonimplicationGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ConverseNonimplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (InputStateA)
            {
                case Trit.Neg: return inputStateB;
                case Trit.Neu: return inputStateB == Trit.Neg ? Trit.Neg : Trit.Neu;
                case Trit.Pos: return Trit.Neg;
            }

            return Trit.Neu;
        }
    }
}
