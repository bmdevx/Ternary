using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components.Adders.Simulated
{
    public class HalfAdder : Basic12In6OutComponent
    {
        public ComponentTriggeredEvent[] CarryOuts { get; }
        
        protected Trit[] CarryOutStates { get; }


        public HalfAdder(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null) : base(aInputStates, bInputStates)
        {
            CarryOuts = new ComponentTriggeredEvent[Tryte.NUMBER_OF_TRITS];
            CarryOutStates = new Trit[Tryte.NUMBER_OF_TRITS];
        }


        public Trit CarryOutState(int pin)
        {
            if (pin >= Tryte.NUMBER_OF_TRITS || pin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            return CarryOutStates[pin];
        }

        
        protected override void AInputInvoked(object sender, Trit trit, int pin)
        {
            AInputStates[pin] = trit;
            Output(pin, AInputStates[pin], BInputStates[pin], Trit.Neu, this);
        }

        protected override void BInputInvoked(object sender, Trit trit, int pin)
        {
            BInputStates[pin] = trit;
            Output(pin, AInputStates[pin], BInputStates[pin], Trit.Neu, this);
        }


        protected void Output(int pin, Trit inputStateA, Trit inputStateB, Trit inputStateCarry, object sender = null)
        {
            Trit sum = inputStateA.Add(inputStateB, ref inputStateCarry);
            Output(pin, sum, sender ?? this);
            CarryOuts[pin]?.Invoke(sender ?? this, inputStateCarry);
        }


        protected override Trit Execute(Trit inputStateA, Trit inputStateB)
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return String.Join(" | ", Enumerable.Range(0, Tryte.NUMBER_OF_TRITS)
                .Select(i => $"{i} {AInputStates[i].ToSymbol()}:{BInputStates[i].ToSymbol()}>{OutputStates[i].ToSymbol()}:{CarryOutStates[i].ToSymbol()}"));
        }
    }
}
