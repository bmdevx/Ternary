﻿using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;

namespace Ternary.Components.Experimental
{
    //output + or 0
    public class MatchGate : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        EqualityGate egatea, egateb, egatec, ecomp1;

        ConsensusGate cgate1, cgate2;

        DeMuxer deMuxer;

        ShiftDownGate shiftDownGate;

        public MatchGate(Trit i0, Trit i1, Trit t2)
        {
            egatea = new EqualityGate(inputStateB: i0);
            egateb = new EqualityGate(inputStateB: i1);
            egatec = new EqualityGate(inputStateB: t2);

            ecomp1 = new EqualityGate(egatec, null, inputStateB: Trit.Pos);

            cgate1 = new ConsensusGate(egatea, egateb);
            cgate2 = new ConsensusGate(cgate1, ecomp1);

            deMuxer = new DeMuxer(inputState: Trit.Pos);

            cgate2.Output += deMuxer.InputSelect;

            shiftDownGate = new ShiftDownGate();

            deMuxer.AOutput += shiftDownGate.Input;
            deMuxer.BOutput += shiftDownGate.Input;
            
            shiftDownGate.Output += (s, t) => Output?.Invoke(this, t);
            deMuxer.COutput += (s, t) => Output?.Invoke(this, t);
        }


        public void InputA(object sender, Trit trit) {
            egatea.AInput(this, trit);
        }

        public void InputB(object sender, Trit trit)
        {
            egateb.AInput(this, trit);
        }

        public void InputC(object sender, Trit trit)
        {
            egatec.AInput(this, trit);
        }
    }
}
