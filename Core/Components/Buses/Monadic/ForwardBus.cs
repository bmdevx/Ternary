﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses.Monadic
{
    public class ForwardDiode : MonadicBaseBus
    {
        protected override Trit Execute(object sender, Trit trit)
        {
            return TritLogic.Forward(trit);
        }
    }
}
