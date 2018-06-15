﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Gates.Monadic
{
    public class InverterGate : MonadicBaseGate
    {
        public InverterGate(IComponentOutput component, Trit inputState = Trit.Neu) :
            base(component, inputState) { }

        public InverterGate(ComponentTriggeredEvent input = null, Trit inputState = Trit.Neu) :
            base(input, inputState) { }

        protected override Trit Execute(object sender, Trit trit)
        {
            return trit.Invert();
        }
    }
}
