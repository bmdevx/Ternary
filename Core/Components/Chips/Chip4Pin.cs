using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Chips
{
    public class Chip4Pin : IChip
    {
        public override int NUMBER_OF_PINS => 4;


        public Chip4Pin(Trit ps1 = Trit.Neu, Trit ps2 = Trit.Neu, Trit ps3 = Trit.Neu, Trit ps4 = Trit.Neu)
        {
            PinStates[0] = ps1;
            PinStates[1] = ps2;
            PinStates[2] = ps3;
            PinStates[3] = ps4;

            Inputs[0] += Pin1Invoked;
            Inputs[1] += Pin2Invoked;
            Inputs[2] += Pin3Invoked;
            Inputs[3] += Pin4Invoked;
        }


        private void Pin1Invoked(object sender, Trit trit)
        {
            PinStates[0] = trit;
            OnPin1Invoked(sender, trit);
        }

        protected virtual void OnPin1Invoked(object sender, Trit trit) { }

        protected void Pin2Invoked(object sender, Trit trit)
        {
            PinStates[1] = trit;
            OnPin2Invoked(sender, trit);
        }

        protected virtual void OnPin2Invoked(object sender, Trit trit) { }

        protected void Pin3Invoked(object sender, Trit trit)
        {
            PinStates[2] = trit;
            OnPin3Invoked(sender, trit);
        }

        protected virtual void OnPin3Invoked(object sender, Trit trit) { }

        protected void Pin4Invoked(object sender, Trit trit)
        {
            PinStates[3] = trit;
            OnPin4Invoked(sender, trit);
        }

        protected virtual void OnPin4Invoked(object sender, Trit trit) { }
    }
}
