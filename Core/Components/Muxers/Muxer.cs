namespace Ternary.Components.Muxers
{
    public class Muxer : MuxerBase, IComponentOutput
    {
        public event ComponentTriggeredEvent Output;

        public event ComponentTriggeredEvent AInput;
        public event ComponentTriggeredEvent BInput;
        public event ComponentTriggeredEvent CInput;

        public Trit InputStateA { get; protected set; }
        public Trit InputStateB { get; protected set; }
        public Trit InputStateC { get; protected set; }


        public Muxer(Trit selectState = Trit.Neu, Trit inputStateA = Trit.Neu, Trit inputStateB = Trit.Neu, Trit inputStateC = Trit.Neu) : base(selectState)
        {
            InputStateA = inputStateA;
            InputStateB = inputStateB;
            InputStateC = inputStateC;

            AInput += AInvoked;
            BInput += BInvoked;
            CInput += CInvoked;
        }


        private void AInvoked(object sender, Trit trit)
        {
            InputA(trit);
        }

        private void BInvoked(object sender, Trit trit)
        {
            InputB(trit);
        }

        private void CInvoked(object sender, Trit trit)
        {
            InputC(trit);
        }


        public void InputA(Trit trit, object sender = null)
        {
            InputStateA = trit;

            if (SelectState == Trit.Neg)
                InvokeOutput(sender ?? this, trit);
        }

        public void InputB(Trit trit, object sender = null)
        {
            InputStateB = trit;

            if (SelectState == Trit.Neu)
                InvokeOutput(sender ?? this, trit);
        }

        public void InputC(Trit trit, object sender = null)
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
            Output?.Invoke(sender, trit);
        }
    }
}
