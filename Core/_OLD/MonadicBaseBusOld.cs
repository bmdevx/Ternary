using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses
{
    public abstract class MonadicBaseBusOld<T> : BasicDataInDataOutComponent<T>, IBusComponent<T> where T : ITernaryDataType, new()
    {
        public event ComponentBusTriggeredEvent<T> BusOutput;


        public MonadicBaseBusOld(IEnumerable<Trit> inputStates = null) :
            base(inputStates) { }


        protected override void OnInputInvoked(object sender, Trit trit, int pin)
        {
            InvokeOutput(pin, Execute(sender, InputStates[pin]), this);
        }


        public void BusInput(object sender, T data)
        {
            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                InputStates[i] = data[i];
            }

            OnBusInputInvoked(sender, data);
        }

        protected virtual void OnBusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus();
        }


        protected void ExecuteInvokeBus()
        {
            for (int i = 0; i < DataType.NUMBER_OF_TRITS; i++)
            {
                OutputStates[i] = Execute(this, InputStates[i]);
                InvokeOutput(i, OutputStates[i], this);
            }

            InvokeBusOutput(BusValue, this);
        }
        
        protected void InvokeBusOutput(T data, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, data);
        }


        protected abstract Trit Execute(object sender, Trit inputState);
    }
}
