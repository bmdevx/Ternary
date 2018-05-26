﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Nonimplication
    public class NonimplicationGate : BasicGate
    {
        public NonimplicationGate(IComponent component1, IComponent component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(component1, component2, input1State, input2State) { }

        public NonimplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(input1, input2, input1State, input2State) { }


        protected override Trit Execute(Trit input1State, Trit input2State)
        {
            switch (input1State)
            {
                case Trit.Neg: return Trit.Neu;
                case Trit.Neu: return input2State == Trit.Pos ? Trit.Neg : Trit.Neu;
                case Trit.Pos: return input2State.Invert();
            }

            return Trit.Neu;
        }
    }
}
