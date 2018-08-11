using System.Diagnostics;

namespace Ternary.Components.Muxers
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class DeMuxer : MuxerBase, IComponentInput
    {
        public event ComponentTriggeredEvent AOutput;
        public event ComponentTriggeredEvent BOutput;
        public event ComponentTriggeredEvent COutput;

        public Trit InputState { get; protected set; }
        public Trit AOutputState { get; protected set; }
        public Trit BOutputState { get; protected set; }
        public Trit COutputState { get; protected set; }

        internal string DebuggerInfo => ToString();
        internal string ComponentName { get; set; }


        public DeMuxer(Trit selectState = Trit.Neu, Trit inputState = Trit.Neu) : base(selectState)
        {
            InputState = inputState;
        }
        

        protected override void OnSelectInvoked(object sender, Trit select)
        {
            Input(this, InputState);
        }
        
        public void Input(object sender, Trit input)
        {
            InputState = input;

            switch (SelectState)
            {
                case Trit.Neg: AOutput?.Invoke(this, AOutputState = input); break;
                case Trit.Neu: BOutput?.Invoke(this, BOutputState = input); break;
                case Trit.Pos: COutput?.Invoke(this, COutputState = input); break;
            }
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {InputState.ToSymbol()}>{AOutputState.ToSymbol()}:{BOutputState.ToSymbol()}:{COutputState.ToSymbol()}";
        }
    }
}
