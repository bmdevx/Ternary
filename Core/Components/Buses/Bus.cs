using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses
{
    public class Bus : MonadicBaseBus
    {
        protected override Trit Execute(object sender, Trit inputState) => inputState;
    }
}
