using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses.Monadic
{
    public class InverterGate : MonadicBaseBus
    {
        protected override Trit Execute(object sender, Trit trit)
        {
            return trit.Invert();
        }
    }
}
