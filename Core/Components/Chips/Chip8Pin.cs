using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Chips
{
    public class Chip8Pin : IMultiIOComponent
    {
        public ComponentTriggeredEvent[] Outputs { get; } = new ComponentTriggeredEvent[8];
        public ComponentTriggeredEvent[] Inputs { get; } = new ComponentTriggeredEvent[8];

        public Trit[] PinStates { get; protected set; } = new Trit[8];


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

            Inputs[0] += Pin1Triggered;
            Inputs[1] += Pin2Triggered;
            Inputs[2] += Pin3Triggered;
            Inputs[3] += Pin4Triggered;
            Inputs[4] += Pin5Triggered;
            Inputs[5] += Pin6Triggered;
            Inputs[6] += Pin7Triggered;
            Inputs[7] += Pin8Triggered;
        }


        private void Pin1Triggered(object sender, Trit trit)
        {
            PinStates[0] = trit;
            OnPin1Triggered(sender, trit);
        }

        protected virtual void OnPin1Triggered(object sender, Trit trit) { }

        protected void Pin2Triggered(object sender, Trit trit)
        {
            PinStates[1] = trit;
            OnPin2Triggered(sender, trit);
        }

        protected virtual void OnPin2Triggered(object sender, Trit trit) { }

        protected void Pin3Triggered(object sender, Trit trit)
        {
            PinStates[2] = trit;
            OnPin3Triggered(sender, trit);
        }

        protected virtual void OnPin3Triggered(object sender, Trit trit) { }

        protected void Pin4Triggered(object sender, Trit trit)
        {
            PinStates[3] = trit;
            OnPin4Triggered(sender, trit);
        }

        protected virtual void OnPin4Triggered(object sender, Trit trit) { }

        protected void Pin5Triggered(object sender, Trit trit)
        {
            PinStates[4] = trit;
            OnPin5Triggered(sender, trit);
        }

        protected virtual void OnPin5Triggered(object sender, Trit trit) { }

        protected void Pin6Triggered(object sender, Trit trit)
        {
            PinStates[5] = trit;
            OnPin6Triggered(sender, trit);
        }

        protected virtual void OnPin6Triggered(object sender, Trit trit) { }

        protected void Pin7Triggered(object sender, Trit trit)
        {
            PinStates[6] = trit;
            OnPin7Triggered(sender, trit);
        }

        protected virtual void OnPin7Triggered(object sender, Trit trit) { }

        protected void Pin8Triggered(object sender, Trit trit)
        {
            PinStates[7] = trit;
            OnPin8Triggered(sender, trit);
        }

        protected virtual void OnPin8Triggered(object sender, Trit trit) { }


        public void Input(int pin, Trit trit, object sender = null)
        {
            Inputs[pin]?.Invoke(sender ?? this, trit);
        }

        protected void Output(int pin, Trit trit, object sender = null)
        {
            Outputs[pin]?.Invoke(sender ?? this, trit);
        }

        public ComponentTriggeredEvent this[int pin]
        {
            get { return Inputs[pin]; }
            set { Outputs[pin] += value; }
        }
    }
}
