using System.Diagnostics;

namespace Ternary.Components.Muxers
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class Muxer : MuxerBase, IComponentOutput
    {
        public event ComponentTriggeredEvent Output;

        public Trit OutputState { get; protected set; }

        public Trit AInputState { get; protected set; }
        public Trit BInputState { get; protected set; }
        public Trit CInputState { get; protected set; }

        internal string DebuggerInfo => ToString();
        public string ComponentName { get; internal set; }


        public Muxer(Trit selectState = Trit.Neu, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu, Trit inputStateC = Trit.Neu) : base(selectState)
        {
            AInputState = inputStateA;
            BInputState = inputStateB;
            CInputState = inputStateC;
        }
       

        public void AInput(object sender, Trit trit)
        {
            AInputState = trit;

            if (SelectState == Trit.Neg)
                InvokeOutput(sender ?? this, trit);
        }

        public void BInput(object sender, Trit trit)
        {
            BInputState = trit;

            if (SelectState == Trit.Neu)
                InvokeOutput(sender ?? this, trit);
        }

        public void CInput(object sender, Trit trit)
        {
            CInputState = trit;

            if (SelectState == Trit.Pos)
                InvokeOutput(sender ?? this, trit);
        }


        protected override void OnSelectInvoked(object sender, Trit select)
        {
            switch (select)
            {
                case Trit.Neg: InvokeOutput(this, AInputState); break;
                case Trit.Neu: InvokeOutput(this, BInputState); break;
                case Trit.Pos: InvokeOutput(this, CInputState); break;
            }
        }
        
        protected void InvokeOutput(object sender, Trit output)
        {
            OutputState = output;
            Output?.Invoke(sender, output);
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {AInputState.ToSymbol()}:{BInputState.ToSymbol()}:{CInputState.ToSymbol()}>{OutputState.ToSymbol()}";
        }
    }
}
