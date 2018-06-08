using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //AntiMax (NOR)
    public class AntiMaxGate : BasicGate
    {
        public AntiMaxGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public AntiMaxGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (inputStateA)
            {
                case Trit.Neg: return inputStateB.Invert();
                case Trit.Neu: return inputStateB == Trit.Pos ? Trit.Neg : Trit.Neu;
                case Trit.Pos: return Trit.Neg;
            }

            return Trit.Neu;
        }
    }
}
