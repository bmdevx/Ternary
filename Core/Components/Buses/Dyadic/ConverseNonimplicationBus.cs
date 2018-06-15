using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class ConverseNonimplicationBus : DyadicBaseBus
    {
        public ConverseNonimplicationBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.ConverseNonimplication(inputStateA, inputStateB);
        }
    }
}
