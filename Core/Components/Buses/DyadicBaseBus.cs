using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ternary.Components.Buses.Dyadic
{
    public abstract class DyadicBaseBus<T> : IBusComponentOutput<T> where T : ITernaryDataType, new()
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<T> BusOutput;

        private T _A, _B;
        

        public void ABusInput(object sender, T data)
        {
            _A = data;

            OnABusInputInvoked(sender, data);
        }

        protected virtual void OnABusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus(_A, _B);
        }


        public void BBusInput(object sender, T data)
        {
            _B = data;

            OnBBusInputInvoked(sender, data);
        }

        protected virtual void OnBBusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus(_A, _B);
        }


        protected void ExecuteInvokeBus(T aData, T bData)
        {
            T oData = new T();

            for (int i = 0; i < oData.NUMBER_OF_TRITS; i++)
            {
                oData[i] = Execute(aData[i], bData[i]);
            }

            InvokeBusOutput(oData, this);
        }
        
        protected void InvokeBusOutput(T data, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, data);
        }


        protected abstract Trit Execute(Trit inputStateA, Trit inputStateB);
    }
}
