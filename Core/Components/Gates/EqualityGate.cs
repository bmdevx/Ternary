using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Consensus
    public class EqualityGate : BasicGate
    {
        public EqualityGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public EqualityGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (InputStateA)
            {
                case Trit.Neg: return inputStateB == Trit.Neg ? Trit.Pos : Trit.Neg;
                case Trit.Neu: return inputStateB == Trit.Neu ? Trit.Pos : Trit.Neg;
                case Trit.Pos: return inputStateB == Trit.Pos ? Trit.Pos : Trit.Neg;
            }

            return Trit.Neu;
        }
    }
}
