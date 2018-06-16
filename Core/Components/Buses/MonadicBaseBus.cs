using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses
{
    public abstract class MonadicBaseBus : Basic6In6OutComponent, IBusComponent
    {
        public event ComponentBusTriggeredEvent BusOutput;


        public MonadicBaseBus(IEnumerable<Trit> inputStates = null) :
            base(inputStates) { }


        protected override void OnInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(sender, InputStates[pin]), this);
        }


        public void BusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                InputStates[i] = tryte[i];
            }

            OnBusInputInvoked(sender, tryte);
        }

        protected virtual void OnBusInputInvoked(object sender, Tryte tryte)
        {
            ExecuteInvokeBus();
        }


        protected void ExecuteInvokeBus()
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                OutputStates[i] = Execute(this, InputStates[i]);
                InvokeOutput(i, OutputStates[i], this);
            }

            InvokeBusOutput(BusValue, this);
        }
        
        protected void InvokeBusOutput(Tryte tryte, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, tryte);
        }


        protected abstract Trit Execute(object sender, Trit inputState);
    }
}
