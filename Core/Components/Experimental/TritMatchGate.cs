using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;

namespace Ternary.Components.Experimental
{
    //output + or 0 depending on if all components have a + input
    public class TritMatchGate : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        private EqualityGate egatea, egateb;
        private ConsensusGate cgate1;
        private DeMuxer deMuxer;
        private ShiftDownGate shiftDownGate;


        public TritMatchGate(Trit i0, Trit i1)
        {
            egatea = new EqualityGate(inputStateB: i0);
            egateb = new EqualityGate(inputStateB: i1);

            cgate1 = new ConsensusGate(egatea, egateb);

            deMuxer = new DeMuxer(inputState: Trit.Pos);

            cgate1.Output += deMuxer.InputSelect;

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
    }
}
