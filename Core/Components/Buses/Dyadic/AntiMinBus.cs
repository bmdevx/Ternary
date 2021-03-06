﻿using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class AntiMinBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public AntiMinBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.AntiMin(inputStateA, inputStateB);
        }
    }
}
