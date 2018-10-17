using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class RegisterCircuit : IBusComponentOutput<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        private TryteRegister register;
        private OutIfPosGate outIfPosGate;


        public RegisterCircuit(IBusComponentOutput<Tryte> dataIn, IComponentOutput rwState, IComponentOutput railX, IComponentOutput railY)
        {
            AddressMatchGate addr = new AddressMatchGate(Trit.Pos, Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;
            addr.InputC(null, Trit.Pos);

            register = new TryteRegister();

            outIfPosGate = new OutIfPosGate(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
