using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //SUM
    public class SumGate : BasicGate
    {
        public SumGate(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(component1, component2, input1State, input2State) { }

        public SumGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(input1, input2, input1State, input2State) { }


        protected override Trit Execute(Trit input1State, Trit input2State)
        {
            switch (input1State)
            {
                case Trit.Neg:
                    {
                        switch (input2State)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Neg;
                            case Trit.Pos: return Trit.Neu;
                        }
                        break;
                    }
                case Trit.Neu: return input2State;
                case Trit.Pos:
                    {
                        switch (input2State)
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
