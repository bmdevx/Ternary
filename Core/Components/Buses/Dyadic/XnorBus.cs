using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class XnorBus : DyadicBaseBus
    {
        public XnorBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Xnor(inputStateA, inputStateB);
        }
    }
}
