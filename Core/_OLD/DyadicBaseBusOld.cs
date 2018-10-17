using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses.Dyadic
{
    public abstract class DyadicBaseBusOld<T> : Basic2DataIn1DataOutComponent<T>, IBusComponentOutput<T> where T : ITernaryDataType, new()
    {
        public event ComponentBusTriggeredEvent<T> BusOutput;
        

        public DyadicBaseBusOld(IEnumerable<Trit> aInputStates = null, IEnumerable<Trit> bInputStates = null) :
            base(aInputStates, bInputStates) { }


        protected override void OnAInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(AInputStates[pin], BInputStates[pin]), this);
        }

        protected override void OnBInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(AInputStates[pin], BInputStates[pin]), sender ?? this);
        }


        public void ABusInput(object sender, T data)
        {
            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                AInputStates[i] = data[i];
            }

            OnABusInputInvoked(sender, data);
        }

        protected virtual void OnABusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus();
        }


        public void BBusInput(object sender, T data)
        {
            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                BInputStates[i] = data[i];
            }

            OnBBusInputInvoked(sender, data);
        }

        protected virtual void OnBBusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus();
        }


        protected void ExecuteInvokeBus()
        {
            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                OutputStates[i] = Execute(AInputStates[i], BInputStates[i]);
                InvokeOutput(i, OutputStates[i], this);
            }

            InvokeBusOutput(BusValue, this);
        }
        
        protected void InvokeBusOutput(T data, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, data);
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);
    }
}
