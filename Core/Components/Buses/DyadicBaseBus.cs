using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses.Dyadic
{
    public abstract class DyadicBaseBus : Basic18In9OutComponent, IBusComponentOutput
    {
        public event ComponentBusTriggeredEvent BusOutput;
        

        public DyadicBaseBus(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null) :
            base(aInputStates, bInputStates) { }


        protected override void OnAInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(AInputStates[pin], BInputStates[pin]), this);
        }

        protected override void OnBInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(AInputStates[pin], BInputStates[pin]), sender ?? this);
        }


        public void ABusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                AInputStates[i] = tryte[i];
            }

            OnABusInputInvoked(sender, tryte);
        }

        protected virtual void OnABusInputInvoked(object sender, Tryte tryte)
        {
            ExecuteInvokeBus();
        }


        public void BBusInput(object sender, Tryte tryte)
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                BInputStates[i] = tryte[i];
            }

            OnBBusInputInvoked(sender, tryte);
        }

        protected virtual void OnBBusInputInvoked(object sender, Tryte tryte)
        {
            ExecuteInvokeBus();
        }


        protected void ExecuteInvokeBus()
        {
            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
            {
                OutputStates[i] = Execute(AInputStates[i], BInputStates[i]);
                InvokeOutput(i, OutputStates[i], this);
            }

            InvokeBusOutput(BusValue, this);
        }
        
        protected void InvokeBusOutput(Tryte tryte, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, tryte);
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);
    }
}
