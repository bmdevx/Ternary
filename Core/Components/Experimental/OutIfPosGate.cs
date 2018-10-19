using Ternary.Components.Muxers;

namespace Ternary.Components.Experimental
{
    public class OutIfPosGate : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        public OutIfPosGate(IComponentOutput evalComponent, IComponentOutput outComponent)
        {
            Muxer muxer = new Muxer();

            outComponent.Output += muxer.CInput;
            evalComponent.Output += muxer.InputSelect;

            muxer.Output += (s, t) => Output?.Invoke(this, t);
        }
    }
}
