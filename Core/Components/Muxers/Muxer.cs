using System.Diagnostics;

namespace Ternary.Components.Muxers
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class Muxer : MuxerBase, IComponentOutput
    {
        public event ComponentTriggeredEvent Output;

        public Trit OutputState { get; protected set; }

        public Trit InputStateA { get; protected set; }
        public Trit InputStateB { get; protected set; }
        public Trit InputStateC { get; protected set; }

        internal string DebuggerInfo => ToString();


        public Muxer(Trit selectState = Trit.Neu, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu, Trit inputStateC = Trit.Neu) : base(selectState)
        {
            InputStateA = inputStateA;
            InputStateB = inputStateB;
            InputStateC = inputStateC;
        }
       

        public void InputA(object sender, Trit trit)
        {
            InputStateA = trit;

            if (SelectState == Trit.Neg)
                InvokeOutput(sender ?? this, trit);
        }

        public void InputB(object sender, Trit trit)
        {
            InputStateB = trit;

            if (SelectState == Trit.Neu)
                InvokeOutput(sender ?? this, trit);
        }

        public void InputC(object sender, Trit trit)
        {
            InputStateC = trit;

            if (SelectState == Trit.Pos)
                InvokeOutput(sender ?? this, trit);
        }


        protected override void OnSelectInvoked(object sender, Trit trit)
        {
            switch (SelectState)
            {
                case Trit.Neg: InvokeOutput(this, InputStateA); break;
                case Trit.Neu: InvokeOutput(this, InputStateB); break;
                case Trit.Pos: InvokeOutput(this, InputStateC); break;
            }
        }
        
        protected void InvokeOutput(object sender, Trit trit)
        {
            OutputState = trit;
            Output?.Invoke(sender, trit);
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {InputStateA.ToSymbol()}:{InputStateB.ToSymbol()}:{InputStateC.ToSymbol()}>{OutputState.ToSymbol()}";
        }
    }
}
