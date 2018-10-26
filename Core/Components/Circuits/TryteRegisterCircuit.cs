using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Storage;

namespace Ternary.Components.Circuits
{
    public class TryteRegisterCircuit : IBusComponentOutput<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        private TryteRegister register;
        private OutIfPosCircuit outIfPosGate;


        public TryteRegisterCircuit(IBusComponentOutput<Tryte> dataIn, IComponentOutput rwState, IComponentOutput railX, IComponentOutput railY)
        {
            TritMatchCircuit addr = new TritMatchCircuit(Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;

            register = new TryteRegister();

            outIfPosGate = new OutIfPosCircuit(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
