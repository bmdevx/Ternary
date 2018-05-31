using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Chips
{
    public class Chip8Pin : IChip
    {
        public override int NUMBER_OF_PINS => 8;


        public Chip8Pin(Trit ps1 = Trit.Neu, Trit ps2 = Trit.Neu, Trit ps3 = Trit.Neu, Trit ps4 = Trit.Neu,
            Trit ps5 = Trit.Neu, Trit ps6 = Trit.Neu, Trit ps7 = Trit.Neu, Trit ps8 = Trit.Neu)
        {
            PinStates[0] = ps1;
            PinStates[1] = ps2;
            PinStates[2] = ps3;
            PinStates[3] = ps4;
            PinStates[4] = ps5;
            PinStates[5] = ps6;
            PinStates[6] = ps7;
            PinStates[7] = ps8;

            Inputs[0] += Pin1Invoked;
            Inputs[1] += Pin2Invoked;
            Inputs[2] += Pin3Invoked;
            Inputs[3] += Pin4Invoked;
            Inputs[4] += Pin5Invoked;
            Inputs[5] += Pin6Invoked;
            Inputs[6] += Pin7Invoked;
            Inputs[7] += Pin8Invoked;
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

        protected void Pin5Invoked(object sender, Trit trit)
        {
            PinStates[4] = trit;
            OnPin5Invoked(sender, trit);
        }

        protected virtual void OnPin5Invoked(object sender, Trit trit) { }

        protected void Pin6Invoked(object sender, Trit trit)
        {
            PinStates[5] = trit;
            OnPin6Invoked(sender, trit);
        }

        protected virtual void OnPin6Invoked(object sender, Trit trit) { }

        protected void Pin7Invoked(object sender, Trit trit)
        {
            PinStates[6] = trit;
            OnPin7Invoked(sender, trit);
        }

        protected virtual void OnPin7Invoked(object sender, Trit trit) { }

        protected void Pin8Invoked(object sender, Trit trit)
        {
            PinStates[7] = trit;
            OnPin8Invoked(sender, trit);
        }

        protected virtual void OnPin8Invoked(object sender, Trit trit) { }
    }
}
