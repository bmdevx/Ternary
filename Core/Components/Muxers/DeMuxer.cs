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
        public Trit OutputStateA { get; protected set; }
        public Trit OutputStateB { get; protected set; }
        public Trit OutputStateC { get; protected set; }

        internal string DebuggerInfo => ToString();


        public DeMuxer(Trit selectState = Trit.Neu, Trit inputState = Trit.Neu) : base(selectState)
        {
            InputState = inputState;
        }
        

        protected override void OnSelectInvoked(object sender, Trit trit)
        {
            Input(this, trit);
        }
        
        public void Input(object sender, Trit trit)
        {
            switch (SelectState)
            {
                case Trit.Neg: AOutput?.Invoke(this, OutputStateA = trit); break;
                case Trit.Neu: BOutput?.Invoke(this, OutputStateB = trit); break;
                case Trit.Pos: COutput?.Invoke(this, OutputStateC = trit); break;
            }
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {InputState.ToSymbol()}>{OutputStateA.ToSymbol()}:{OutputStateB.ToSymbol()}:{OutputStateC.ToSymbol()}";
        }
    }
}
