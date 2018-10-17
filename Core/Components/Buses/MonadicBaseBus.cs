using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Tools;

namespace Ternary.Components.Buses
{
    public abstract class MonadicBaseBus<T> : IBusComponent<T> where T : ITernaryDataType
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<T> BusOutput;
        

        public void BusInput(object sender, T data)
        {
            OnBusInputInvoked(sender, data);
        }

        protected virtual void OnBusInputInvoked(object sender, T data)
        {
            ExecuteInvokeBus((T)data.CreateFromTrits(data.Select(d => Execute(this, d))));
        }


        protected void ExecuteInvokeBus(T data)
        {
            InvokeBusOutput(data);
        }
        
        protected void InvokeBusOutput(T data, object sender = null)
        {
            BusOutput?.Invoke(sender ?? this, data);
        }


        protected abstract Trit Execute(object sender, Trit inputState);
    }
}
