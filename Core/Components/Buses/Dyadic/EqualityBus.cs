using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class EqualityBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public EqualityBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Equality(inputStateA, inputStateB);
        }
    }
}
