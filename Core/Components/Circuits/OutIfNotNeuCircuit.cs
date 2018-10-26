using Ternary.Components.Muxers;

namespace Ternary.Components.Circuits
{
    public class OutIfNotNeuCircuit : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        public OutIfNotNeuCircuit(TritMatchCircuit3 matchGate, IComponentOutput componentOutput)
        {
            Muxer muxer = new Muxer();

            componentOutput.Output += muxer.AInput;
            componentOutput.Output += muxer.CInput;
            matchGate.Output += muxer.InputSelect;

            muxer.Output += (s, t) => Output?.Invoke(this, t);
        }
    }
}
