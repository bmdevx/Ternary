﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates
{
    //Converse Implication
    public class ConverseImplicationGate : BasicGate
    {
        public ConverseImplicationGate(IComponent componentA, IComponent componentB, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(componentA, componentB, inputStateA, inputStateB) { }

        public ConverseImplicationGate(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu) :
            base(input1, input2, inputStateA, inputStateB) { }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            switch (inputStateA)
            {
                case Trit.Neg: return inputStateB.Invert();
                case Trit.Neu: return inputStateB == Trit.Neg ? Trit.Pos : Trit.Neu;
                case Trit.Pos: return Trit.Pos;
            }

            return Trit.Neu;
        }
    }
}
