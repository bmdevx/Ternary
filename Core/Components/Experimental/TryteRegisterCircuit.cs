using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class RegisterCircuit : IBusComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent BusOutput;

        private TryteRegister register;
        private OutIfPosGate outIfPosGate;
        private Trit enableState;


        public RegisterCircuit(IBusComponentOutput dataIn, IComponentOutput rwState, IComponentOutput railX, IComponentOutput railY, IComponentOutput railZ)
        {
            MatchGate addr = new MatchGate(Trit.Pos, Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;
            railZ.Output += addr.InputC;

            register = new TryteRegister();

            outIfPosGate = new OutIfPosGate(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
