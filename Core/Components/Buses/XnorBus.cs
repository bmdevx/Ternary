using System.Collections.Generic;

namespace Ternary.Components.Buses
{
    public class XnorBus : BusBase
    {
        public XnorBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Xnor(inputStateA, inputStateB);
        }
    }
}
