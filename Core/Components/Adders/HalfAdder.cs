using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;

namespace Ternary.Components.Adders
{
    public class HalfAdder : IAdder
    {
        private Muxer carryMuxer = new Muxer();
        private Muxer sumMuxer = new Muxer();

        private ForwardDiode forwardDiode;
        private ReverseDiode reverseDiode;
        private CycleDownGate cycleDownGate;
        private CycleUpGate cycleUpGate;
        

        public HalfAdder()
        {
            forwardDiode = new ForwardDiode(_WireInA);
            reverseDiode = new ReverseDiode(_WireInA);
            cycleDownGate = new CycleDownGate(_WireInA);
            cycleUpGate = new CycleUpGate(_WireInA);

            _WireInA.Output += sumMuxer.BInput;

            _WireInB.Output += sumMuxer.InputSelect;
            _WireInB.Output += carryMuxer.InputSelect;
            
            reverseDiode.Output += carryMuxer.AInput;
            forwardDiode.Output += carryMuxer.CInput;

            cycleDownGate.Output += sumMuxer.AInput;
            cycleUpGate.Output += sumMuxer.CInput;

            sumMuxer.Output += InvokeSumOutput;
            carryMuxer.Output += InvokeCarryOutput;
        }
    }
}
