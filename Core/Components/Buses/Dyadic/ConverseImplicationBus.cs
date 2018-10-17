﻿using System.Collections.Generic;

namespace Ternary.Components.Buses.Dyadic
{
    public class ConverseImplicationBus<T> : DyadicBaseBus<T> where T : ITernaryDataType, new()
    {
        //public ConverseImplicationBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.ConverseImplication(inputStateA, inputStateB);
        }
    }
}
