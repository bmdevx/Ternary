namespace Ternary.Components.Chips
{
    public class Chip8Pin : IChip
    {
        public override int NUMBER_OF_PINS => 8;


        public Chip8Pin(Trit ps0 = Trit.Neu, Trit ps1 = Trit.Neu, Trit ps2 = Trit.Neu, Trit ps3 = Trit.Neu,
            Trit ps4 = Trit.Neu, Trit ps5 = Trit.Neu, Trit ps6 = Trit.Neu, Trit ps7 = Trit.Neu)
        {
            PinStates[0] = ps0;
            PinStates[1] = ps1;
            PinStates[2] = ps2;
            PinStates[3] = ps3;
            PinStates[4] = ps4;
            PinStates[5] = ps5;
            PinStates[6] = ps6;
            PinStates[7] = ps7;

            Inputs[0] += Pin0Invoked;
            Inputs[1] += Pin1Invoked;
            Inputs[2] += Pin2Invoked;
            Inputs[3] += Pin3Invoked;
            Inputs[4] += Pin4Invoked;
            Inputs[5] += Pin5Invoked;
            Inputs[6] += Pin6Invoked;
            Inputs[7] += Pin7Invoked;
        }


        private void Pin0Invoked(object sender, Trit trit)
        {
            PinStates[0] = trit;
            OnPin0Invoked(sender, trit);
        }

        protected virtual void OnPin0Invoked(object sender, Trit trit) { }

        protected void Pin1Invoked(object sender, Trit trit)
        {
            PinStates[1] = trit;
            OnPin1Invoked(sender, trit);
        }

        protected virtual void OnPin1Invoked(object sender, Trit trit) { }

        protected void Pin2Invoked(object sender, Trit trit)
        {
            PinStates[2] = trit;
            OnPin2Invoked(sender, trit);
        }

        protected virtual void OnPin2Invoked(object sender, Trit trit) { }

        protected void Pin3Invoked(object sender, Trit trit)
        {
            PinStates[3] = trit;
            OnPin3Invoked(sender, trit);
        }

        protected virtual void OnPin3Invoked(object sender, Trit trit) { }

        protected void Pin4Invoked(object sender, Trit trit)
        {
            PinStates[4] = trit;
            OnPin4Invoked(sender, trit);
        }

        protected virtual void OnPin4Invoked(object sender, Trit trit) { }

        protected void Pin5Invoked(object sender, Trit trit)
        {
            PinStates[5] = trit;
            OnPin5Invoked(sender, trit);
        }

        protected virtual void OnPin5Invoked(object sender, Trit trit) { }

        protected void Pin6Invoked(object sender, Trit trit)
        {
            PinStates[6] = trit;
            OnPin6Invoked(sender, trit);
        }

        protected virtual void OnPin6Invoked(object sender, Trit trit) { }

        protected void Pin7Invoked(object sender, Trit trit)
        {
            PinStates[7] = trit;
            OnPin7Invoked(sender, trit);
        }

        protected virtual void OnPin7Invoked(object sender, Trit trit) { }
    }
}
