using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses.Dyadic
{
    public abstract class DyadicBaseBus : Basic12In6OutComponent
    {
        public DyadicBaseBus(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null) :
            base(aInputStates, bInputStates) { }
    }
}
