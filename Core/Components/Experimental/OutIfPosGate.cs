using Ternary.Components.Muxers;

namespace Ternary.Components.Experimental
{
    public class OutIfPosGate : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        public OutIfPosGate(MatchGate matchGate, IComponentOutput componentOutput)
        {
            Muxer muxer = new Muxer();

            componentOutput.Output += muxer.CInput;
            matchGate.Output += muxer.InputSelect;

            muxer.Output += (s, t) => Output?.Invoke(this, t);
        }
    }
}
