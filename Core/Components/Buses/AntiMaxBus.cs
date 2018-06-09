﻿using System.Collections.Generic;

namespace Ternary.Components.Buses
{
    public class AntiMaxBus : BusBase
    {
        public AntiMaxBus(IEnumerable<Trit> aPinStates = null, IEnumerable<Trit> bPinStates = null) : base(aPinStates, bPinStates) { }

        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            return TritLogic.AntiMax(inputStateA, inputStateB);
        }
    }
}
