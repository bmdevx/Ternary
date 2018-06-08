using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Implication
    public class ImplicationGate : BasicGate
    {
        public ImplicationGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ImplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (inputStateA)
            {
                case Trit.Neg: return Trit.Pos;
                case Trit.Neu: return inputStateB == Trit.Pos ? Trit.Pos : Trit.Neu;
                case Trit.Pos: return inputStateB;
            }

            return Trit.Neu;
        }
    }
}
