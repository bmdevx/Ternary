using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Consensus
    public class ConsensusGate : BasicGate
    {
        public ConsensusGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ConsensusGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (inputStateA)
            {
                case Trit.Neg: return inputStateB == Trit.Neg ? Trit.Neg : Trit.Neu;
                case Trit.Neu: return Trit.Neu;
                case Trit.Pos: return inputStateB == Trit.Pos ? Trit.Pos : Trit.Neu;
            }

            return Trit.Neu;
        }
    }
}
