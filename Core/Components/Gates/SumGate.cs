using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //SUM
    public class SumGate : BasicGate
    {
        public SumGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public SumGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (inputStateA)
            {
                case Trit.Neg:
                    {
                        switch (inputStateB)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Neg;
                            case Trit.Pos: return Trit.Neu;
                        }
                        break;
                    }
                case Trit.Neu: return inputStateB;
                case Trit.Pos:
                    {
                        switch (inputStateB)
                        {
                            case Trit.Neg: return Trit.Neu;
                            case Trit.Neu: return Trit.Pos;
                            case Trit.Pos: return Trit.Neg;
                        }
                        break;
                    }
            }

            return Trit.Neu;
        }
    }
}
