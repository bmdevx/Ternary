using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class MinBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public MinBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Min(inputStateA, inputStateB);
        }
    }
}
