using Ternary.Components.Muxers;

namespace Ternary.Components.Circuits
{
    public class OutIfPosCircuit : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        public OutIfPosCircuit(IComponentOutput evalComponent, IComponentOutput outComponent)
        {
            Muxer muxer = new Muxer();

            outComponent.Output += muxer.CInput;
            evalComponent.Output += muxer.InputSelect;

            muxer.Output += (s, t) => Output?.Invoke(this, t);
        }
    }
}
