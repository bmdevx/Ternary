﻿using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class NonimplicationBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public NonimplicationBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.Nonimplication(inputStateA, inputStateB);
        }
    }
}
