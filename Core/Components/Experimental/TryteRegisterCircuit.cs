﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TryteRegisterCircuit : IBusComponentOutput<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        private TryteRegister register;
        private OutIfPosGate outIfPosGate;


        public TryteRegisterCircuit(IBusComponentOutput<Tryte> dataIn, IComponentOutput rwState, IComponentOutput railX, IComponentOutput railY)
        {
            TritMatchGate addr = new TritMatchGate(Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;

            register = new TryteRegister();

            outIfPosGate = new OutIfPosGate(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
