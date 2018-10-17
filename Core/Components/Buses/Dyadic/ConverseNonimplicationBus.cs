using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class ConverseNonimplicationBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public ConverseNonimplicationBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.ConverseNonimplication(inputStateA, inputStateB);
        }
    }
}
