﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Old
{
    public class Bus : MonadicBaseBus
    {
        protected override Trit Execute(object sender, Trit inputState) => inputState;
    }
}
