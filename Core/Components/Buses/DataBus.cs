using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses
{
    public class DataBus<T> : MonadicBaseBus<T> where T : ITernaryDataType, new()
    {
        protected override Trit Execute(object sender, Trit inputState) => inputState;
    }
}
