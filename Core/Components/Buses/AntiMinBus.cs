using System.Collections.Generic;

namespace Ternary.Components.Buses
{
    public class AntiMinBus : BusBase
    {
        public AntiMinBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.AntiMin(inputStateA, inputStateB);
        }
    }
}
