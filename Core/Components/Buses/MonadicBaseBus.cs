using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses
{
    public abstract class MonadicBaseBus : Basic6In6OutComponent
    {
        public MonadicBaseBus(IEnumerable<Trit> inputStates = null) :
            base(inputStates) { }
    }
}
