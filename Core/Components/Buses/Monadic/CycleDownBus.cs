using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses.Monadic
{
    public class CycleDownBus<T> : MonadicBaseBus<T> where T : ITernaryDataType, new()
    {
        protected override Trit Execute(object sender, Trit trit)
        {
            return TritLogic.CycleDown(trit);
        }
    }
}
