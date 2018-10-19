using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TrortRegisterCircuit : IBusComponentOutput<Trort>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Trort> BusOutput;

        private TrortRegister register;
        private OutIfPosGate outIfPosGate;


        public TrortRegisterCircuit(IBusComponentOutput<Trort> dataIn, IComponentOutput rwState,
            IComponentOutput railX, IComponentOutput railY, IComponentOutput railZ, IComponentOutput railT)
        {
            TritMatchGate4 addr = new TritMatchGate4(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;
            railZ.Output += addr.InputC;
            railT.Output += addr.InputD;

            register = new TrortRegister();

            outIfPosGate = new OutIfPosGate(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
