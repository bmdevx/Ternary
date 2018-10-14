using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Tools;

namespace Ternary.Components.Adders.Simulated
{
    public class TryteFullAdder : TryteHalfAdder
    {
        public ComponentTriggeredEvent[] CarryInputs { get; }

        protected Trit[] CarryInputStates { get; }


        public TryteFullAdder(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null, IEnumerable<Trit> carryInputStates = null) :
            base(aInputStates, bInputStates)
        {
            CarryInputs = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            CarryInputStates = new Trit[Tryte.NUMBER_OF_TRITS];

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                CarryInputs[i] += (s, t) => CarryInInvoked(s, t, i);
            }
        }


        public Trit CarryInputState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return CarryInputStates[pin];
        }


        protected override void OnAInputInvoked(object sender, Trit trit, int pin)
        {
            AInputStates[pin] = trit;
            Output(pin, AInputStates[pin], BInputStates[pin], CarryInputStates[pin], this);
        }

        protected override void OnBInputInvoked(object sender, Trit trit, int pin)
        {
            BInputStates[pin] = trit;
            Output(pin, AInputStates[pin], BInputStates[pin], CarryInputStates[pin], this);
        }

        protected void CarryInInvoked(object sender, Trit trit, int pin)
        {
            CarryInputStates[pin] = trit;
            Output(pin, AInputStates[pin], BInputStates[pin], CarryInputStates[pin], this);
        }


        public override string ToString()
        {
            return String.Join(" | ",
                Create.NewTryteSizedArray(i => $"{i} {AInputStates[i].ToSymbol()}:{BInputStates[i].ToSymbol()}:{CarryInputStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}:{CarryOutStates[i].ToSymbol()}"));
        }
    }
}
