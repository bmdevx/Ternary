using System.Collections.Generic;

namespace Ternary.Components.Buses
{
    public class SumBus : BusBase
    {
        public SumBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Sum(inputStateA, inputStateB);
        }
    }
}
