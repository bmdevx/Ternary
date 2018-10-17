using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class XorBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public XorBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Xor(inputStateA, inputStateB);
        }
    }
}
