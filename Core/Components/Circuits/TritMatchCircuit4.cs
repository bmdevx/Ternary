using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;

namespace Ternary.Components.Circuits
{
    //output + or 0 depending on if all components have a + input
    public class TritMatchCircuit4 : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        private EqualityGate egatea, egateb, egatec, egated;
        private ConsensusGate cgate1, cgate2, cgate3;
        private DeMuxer deMuxer;
        private ShiftDownGate shiftDownGate;


        public TritMatchCircuit4(Trit t0, Trit t1, Trit t2, Trit t3)
        {
            egatea = new EqualityGate(inputStateB: t0) { ComponentName = nameof(egatea) };
            egateb = new EqualityGate(inputStateB: t1) { ComponentName = nameof(egateb) };
            egatec = new EqualityGate(inputStateB: t2) { ComponentName = nameof(egatec) };
            egated = new EqualityGate(inputStateB: t3) { ComponentName = nameof(egated) };

            cgate1 = new ConsensusGate(egatea, egateb) { ComponentName = nameof(cgate1) };
            cgate2 = new ConsensusGate(egatec, egated) { ComponentName = nameof(cgate2) };

            cgate3 = new ConsensusGate(cgate1, cgate2) { ComponentName = nameof(cgate3) };

            deMuxer = new DeMuxer(inputState: Trit.Pos);

            cgate3.Output += deMuxer.InputSelect;

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

        public void InputD(object sender, Trit trit)
        {
            egated.AInput(this, trit);
        }
    }
}
