using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses
{
    public abstract class BusBase : Basic12In6OutComponent
    {
        public BusBase(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null) :
            base(aInputStates, bInputStates) { }
    }
}
