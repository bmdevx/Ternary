using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TrortAddrTryteRegisterCircuit : IBusComponentOutput<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        private TryteRegister register;
        private OutIfPosGate outIfPosGate;


        public TrortAddrTryteRegisterCircuit(IBusComponentOutput<Tryte> dataIn, IComponentOutput rwState,
            IComponentOutput railX, IComponentOutput railY, IComponentOutput railZ, IComponentOutput railT)
        {
            TritMatchGate4 addr = new TritMatchGate4(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;
            railZ.Output += addr.InputC;
            railT.Output += addr.InputD;

            register = new TryteRegister();

            outIfPosGate = new OutIfPosGate(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
